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
        private ExamTrainerContext _data = new ExamTrainerContext();
        
        [HttpPost]
        public String PostToAddQuestion(Question question)
        {
            System.Diagnostics.Debug.WriteLine("PostToAddQuestion() called with:");
            System.Diagnostics.Debug.WriteLine(question.ToString());
            
            return "AddQuestion controller response";
        }
    }
}
