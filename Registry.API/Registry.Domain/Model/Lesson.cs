using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry.Domain.Model
{
  public class Lesson
  {
    [Key]
    public int Id { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string LessonType { get; set; }
       

        //Свойства навигации
        public Discipline Discipline { get; set; }
        public int DisciplineId { get; set; }
    }
}
