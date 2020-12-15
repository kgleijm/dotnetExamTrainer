with open("multiple_choice_all_questions2020.txt", "r") as file:
    rawQuestions = file.read().replace("-" * 27, "-" * 28).split("-" * 28)[1:]
    i = 0
    for rawQuestion in rawQuestions:
        i += 1
        try:
            questionAsDict = dict()
            garbage, question, answers = rawQuestion.split('\n', 2)
            Id, QuestionText = question.split(":", 1)
            Id = int("".join(filter(str.isdigit, Id)))
            QuestionText = QuestionText[1:]
            if QuestionText[-1] == " ":
                QuestionText = QuestionText[0:-1]
            # print("'" + str(Id) + "' '" + QuestionText + "'\n", answers)
            print("\n")

            
            answer =



            questionAsDict["Id"] = Id
            questionAsDict["QuestionText"] = QuestionText

        except Exception as e:
            print("\nmissing question due to inconsistent formatting\n")
    print("\nactually found", i, "questions")

