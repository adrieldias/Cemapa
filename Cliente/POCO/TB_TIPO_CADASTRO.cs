using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.POCO
{
    public class TB_TIPO_CADASTRO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_TIPO_CADASTRO()
        {
            this.TB_CADASTRO = new HashSet<TB_CADASTRO>();
        }

        public int COD_TIPO_CADASTRO { get; set; }
        public string DESC_TIPO_CADASTRO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_CADASTRO> TB_CADASTRO { get; set; }
    }
}
