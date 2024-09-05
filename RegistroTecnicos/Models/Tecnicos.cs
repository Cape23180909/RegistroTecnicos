﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Tecnicos
{
    [Key]
    public int TecnicoId { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public string? Nombres { get; set; }
    [Required(ErrorMessage = "Llenar este campo por favor.")]
    public decimal? Sueldohora { get; set; }
    [ForeignKey("TiposTecnicos")]
    public int TipoId { get; set; }
    public TiposTecnicos? TiposTecnicos { get; set; }
}