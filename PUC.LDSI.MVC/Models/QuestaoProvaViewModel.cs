using PUC.LDSI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUC.LDSI.MVC.Models
{
    public class QuestaoProvaViewModel
    {
        public int Id { get; set; }
        public int ProvaId { get; set; }
        public int Tipo { get; set; }
        public int Numero { get; set; } 
        public int Avancar { get; set; }
        public List<OpcaoProvaViewModel> OpcoesProva { get; set; }
        public string EnunciadoQuestao { get; set; }
        public string DescricaoAvaliacao { get; } 

        public bool EhValida
        {
            get
            {
                if (Tipo == 1) return true; 
                int contagem = OpcoesProva.FindAll(n => n.Resposta == true).Count;

                if (contagem == 1) return true;
                return false;
            }
        }

    }
}
