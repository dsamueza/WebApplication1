using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ValidateModel
    {
     public Guid Id { set; get; }
     public string  AggregateUri  { set; get; }
     public bool  typeObs  { set; get; }
     public string description  { set; get; }
     public string user_web  { set; get; }
     public DateTime DateValidation  { set; get; }

    }
}