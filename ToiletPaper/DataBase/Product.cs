//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToiletPaper.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.MaterialsAndProducts = new HashSet<MaterialsAndProducts>();
        }
    
        public int Id_Prod { get; set; }
        public string Name { get; set; }
        public Nullable<int> MinCostForAgent { get; set; }
        public byte[] Picture { get; set; }
        public int Id_Type { get; set; }
        public Nullable<int> Count { get; set; }
        public int Id_Material { get; set; }
    
        public virtual Material Material { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MaterialsAndProducts> MaterialsAndProducts { get; set; }
        public virtual TypeProd TypeProd { get; set; }
    }
}
