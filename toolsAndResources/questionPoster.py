import json
from time import sleep

import requests
import urllib3
from urllib3.exceptions import InsecureRequestWarning
urllib3.disable_warnings(InsecureRequestWarning)

import psycopg2
from psycopg2.extensions import ISOLATION_LEVEL_AUTOCOMMIT
db = None
cursor = None

db = psycopg2.connect(user="postgres", password="none", database="dotnetExamTrainer")
db.set_isolation_level(ISOLATION_LEVEL_AUTOCOMMIT)
cursor = db.cursor()


with open("multiple_choice_all_questions2020.txt", "r") as file:
    rawQuestions = file.read().replace("-" * 27, "-" * 28).replace("-" * 26, "-" * 28).replace("-" * 29, "-" * 28).split("-" * 28)[1:]
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
            # print("\n")
            # print(rest)
            # print("\n\n")
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

            questionAsDict["Id"] = Id
            questionAsDict["QuestionText"] = QuestionText
            questionAsDict["SubText"] = subtext
            questionAsDict["AnswerText"] = answers
            questionAsDict["RightAnswer"] = rightAnswer

            try:
                textarr = "ARRAY " + str(answers)  # .replace("[", "{").replace("]", "}").replace("'", '"')
                createSQL = "INSERT INTO \"Questions\" VALUES(" + str(questionAsDict["Id"]) + ", '" + questionAsDict["QuestionText"] + "', " + textarr + ", " + str(questionAsDict["RightAnswer"]) + ", '" + questionAsDict["SubText"]+ "')"
                createSQL += " ON CONFLICT DO NOTHING"
                # print(createSQL)
                cursor.execute(createSQL)
                print(Id, "successfully added item to database")
            except Exception as e:
                # print(e)
                print(Id, "not successfully added\t\t", QuestionText.replace("\n", "   "), "\t", textarr.replace("\n", "   "), "\n", str(e).replace("\n", "   "), "\n", createSQL.replace("\n", "   "))
                # print("\n\nSQL:\n" + createSQL + "\n\n:End of SQL\n")
                # print("'" + str(Id) + "' '" + QuestionText + "'\n", answers)
                # print("\n")
                # print(rest)
                # print("\n\n")



            # questionAsJson = json.dumps(questionAsDict)
            # headers = {'Content-type': 'application/json', 'Host': 'localhost:5001'}
            # my_url = 'https://localhost:5001/addquestion'
            #
            #
            #
            # r = requests.post(my_url, json=questionAsJson, verify=False,)
            # print(r.status_code)
            # print(r.headers)
            # print(r.json())
            # print(questionAsJson)

        except Exception as e:
            print(e)
            print("\nmissing question due to inconsistent formatting\n")
    print("\nactually found", i, "questions")

