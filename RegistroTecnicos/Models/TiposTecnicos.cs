﻿using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace RegistroTecnicos.Models;

public class TiposTecnicos
{
    [Key]
    public int TipoTecnicoId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string Descripcion { get; set; }
    public Tecnicos? Tecnicos { get; set; }
}