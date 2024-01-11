﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Squadmakers.Infraestructure.Models;

public partial class Usuario
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(255)]
    public string Contraseña { get; set; }

    [InverseProperty("Usuario")]
    public virtual ICollection<Chiste> Chistes { get; set; } = new List<Chiste>();
}