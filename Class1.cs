using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp20
{
    public partial class Операции
    {
        public override string ToString()
        {
            return Тип_операции;
        }
    }
 

  
    public partial class Оборудование
    {
        public override string ToString()
        {
            return Название_;
        }
    }
    public partial class Сотрудники
    {
        public override string ToString()
        {
            return ФИО;
        }
    }
    public partial class Поставщики
    {
        public override string ToString()
        {
            return Название_поставщика_;
        }
    }
}