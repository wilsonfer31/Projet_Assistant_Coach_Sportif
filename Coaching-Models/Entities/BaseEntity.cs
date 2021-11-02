using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coaching_Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public BaseEntity(int id)
        {
            Id = id;
        }
    }
}
