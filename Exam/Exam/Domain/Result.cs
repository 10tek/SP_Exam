using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.Domain
{
    public class Result
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string Text { get; set; }
    }
}
