using System;
using Domain.Commands;

namespace Presentation.Models
{
    public class EnrollmentRendering
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public static class EnrollmentRenderingExtensions
    {
        public static StudentEnrollCommand ToCommand(this EnrollmentRendering rendering)
        {
            return new StudentEnrollCommand(rendering.StudentId, rendering.ClassId, rendering.From, rendering.To);
        }
    }
}