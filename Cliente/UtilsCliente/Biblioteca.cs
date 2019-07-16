using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;
 //System.Globalization.CultureInfo.GetCultureInfo("pt-BR")

namespace Cliente.UtilsCliente
{
	public class Biblioteca
	{
	}

	public class RemoveAcento
	{
		public static string RemoveAcentoTexto(string texto)
		{
			string comAcentos = "éíóúàèìòùâêîôûäëïöüõñçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÄËÏÖÜÃÕÑÇ&*_-@#$|°ª";
			string semAcentos = "aeiouaeiouaeiouaeiouoncAEIOUAEIOUAEIOUAEIOUAONC          ";
			for (int i = 0; i < comAcentos.Length; i++) 
			{
				texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
			}
			return texto;
		}
	}

	public class DataPorExtenso
	{
		public static string DataPorExtenso1(string data)
		{
			CultureInfo culture = new CultureInfo("pt-BR"); //especifica o padrão de data para determiado país.
			DateTimeFormatInfo dtfi = culture.DateTimeFormat;
			int dia = DateTime.Now.Day;
			int ano = DateTime.Now.Year;
			string mes = culture.TextInfo.ToTitleCase(dtfi.GetMonthName(DateTime.Now.Month));
			string diasemana = culture.TextInfo.ToTitleCase(dtfi.GetDayName(DateTime.Now.DayOfWeek));
			return data = diasemana + ", " + dia + " de " + mes + " de " + ano;
		    //Data.ToString("dddd, dd 'de' MMMM 'de' yyyy", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
		}
	}
}
