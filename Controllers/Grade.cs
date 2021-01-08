﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using dotnetExamTrainer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dotnetExamTrainer.Controllers
{
    [Route("Home/[controller]")]
    public class GradeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public class Gradeable
        {
            class Ans
            {
                public int RightAnswer { get; set; }
            }
            
            private Object[] AnswerList {get; set;}

            public void GradeAnswers()
            {
                Console.WriteLine("started grading answers");
                for (int i = 0; i < AnswerList.Length; i++)
                {
                    var a =((Ans)AnswerList[i]).RightAnswer;
                    Console.WriteLine("RightAnswer = " + a);
                    
                }
            }
        }
        
        public GradeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public string Post([FromBody] JsonElement inp)
        {
            var str = inp.ToString();
            JObject json = JObject.Parse(str);
            var ansList = json["AnswerList"].ToList();
            for (int i = 0; i < ansList.Count; i++)
            {
                ansList[i]["RightAnswer"] = 10;
                Console.WriteLine(ansList[i]);
            }
            json["AnswerList"] = JArray.FromObject(ansList);
            Console.WriteLine("OutList: " + json["AnswerList"]);
            string outp = json.ToString();
            Console.WriteLine("\n\nOutput: " + json["AnswerList"]);
            return outp;
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}