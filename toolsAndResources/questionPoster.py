import json

with open("multiple_choice_all_questions2020.txt", "r") as file:
    rawQuestions = file.read().replace("-" * 27, "-" * 28).split("-" * 28)[1:]
    i = 0
    for rawQuestion in rawQuestions:
        i += 1
        try:
            questionAsDict = dict()
            garbage, question, rest = rawQuestion.split('\n', 2)
            Id, QuestionText = question.split(":", 1)
            Id = int("".join(filter(str.isdigit, Id)))
            QuestionText = QuestionText[1:]
            if QuestionText[-1] == " ":
                QuestionText = QuestionText[0:-1]
            # print("'" + str(Id) + "' '" + QuestionText + "'\n", answers)
            #print("\n")
            #print(rest)
            restLines = rest.split("\n")
            answers = []
            subtext = ""
            rightAnswer = -1
            ans = 0
            for line in restLines:

                if len(line) > 1:   # non-empty line
                    while line[-1] == " ":
                        line = line[0: -1]
                    if line[0] == "-":  # answer line
                        if line[-1] == "*":   # right answer line
                            rightAnswer = ans
                            answers.append(line[1:-1])
                        else:
                            answers.append(line[1:])
                        ans += 1
                    else:  # subtext line
                            subtext += line + "\n"


            questionAsDict["RightAnswer"] = rightAnswer
            questionAsDict["AnswerText"] = answers
            questionAsDict["Subtext"] = subtext
            questionAsDict["Id"] = Id
            questionAsDict["QuestionText"] = QuestionText
            #
            # if rightAnswer < 0:
            #     print("\n\nHas wrong ans amount > " + str(Id), "\n\n", "Amount: ", len(answers))
            #     print(QuestionText)
            #     for a in answers:
            #         print(a)


            questionAsJson = json.dumps(questionAsDict)
            print(questionAsJson)

        except Exception as e:
            print("\nmissing question due to inconsistent formatting\n")
    print("\nactually found", i, "questions")

