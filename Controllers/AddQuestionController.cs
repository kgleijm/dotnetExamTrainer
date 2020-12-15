using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetExamTrainer.Models;

namespace dotnetExamTrainer.Controllers
{
    
    
    
    
    [Route("[controller]")]
    [ApiController]
    public class AddQuestionController : ControllerBase
    {
        
        
        [HttpPost]
        public String PostToAddQuestion(Question question)
        {
            System.Diagnostics.Debug.WriteLine("PostToAddQuestion() called with:");
            System.Diagnostics.Debug.WriteLine(question.ToString());
            
            using(ExamTrainerContext db = new ExamTrainerContext())
            {
                if (!db.Questions.Select(q => q.Id).Contains(question.Id))
                {
                    db.Add(question);
                    db.SaveChanges();
                    return "Success\nAdded question: " + question.ToString() + " to database";
                }
                else
                {
                    return "Failed\nQuestion: " + question.ToString() + " already in database";
                }
            }
            
            
            return "AddQuestion controller response";
        }
    }
}
