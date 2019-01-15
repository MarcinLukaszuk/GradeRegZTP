using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Builder
{
    public interface ITimetableBuilder
    {
        void AddHeader();
        void AddColumn(string columnName);
        void AddRow();
    }
}