﻿namespace EnigmaClass
{
    public class Desempenho
    {
        public int ID { get; set; }
        public Usuario Usuario { get; set; }
        public Materia Materia { get; set; }
        public decimal Porcentagem { get; set; }
        public decimal HorasEstudadas { get; set; }
    }
}
