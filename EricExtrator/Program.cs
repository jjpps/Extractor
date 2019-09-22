using Entidades;
using Entidades.Util;
using HtmlAgilityPack;
using NeatExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EricExtrator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Iniciando Bots");

            List<Imovel> Imoveis = new List<Imovel>();
            for (int i = 1; i <= 109; i++)
            //for (int i = 1; i <= 2; i++)
            {
                string site = string.Empty;
                site = string.Format(@"https://www.colnaghi.com.br/Busca?Pagina={0}", i);
                var valores = GetHtmlFromWeb(site).DocumentNode.SelectNodes("//*/a/div/div[1]/h5[2]/strong");
                var Codigos = GetHtmlFromWeb(site).DocumentNode.SelectNodes("//*/a/div/div[1]/span").Where(c => !string.IsNullOrEmpty(c.InnerText.Trim())).ToList(); ;
                var Tamanhos = GetHtmlFromWeb(site).DocumentNode.SelectNodes("//*/a/div/div[1]/ul/li");

                int id = 0;
                foreach (var item in Codigos)
                {
                    Imovel imovel = new Imovel();
                    try
                    {
                        imovel.Codigo = item.InnerText;
                        imovel.id_imovel = id++;
                        //bairro
                        imovel.Bairro = ConvertEncoding(GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[1]/h5[1]/strong", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim());
                        //categoria
                        imovel.Categoria = ConvertEncoding(GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[2]/span", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim());
                        //valores 
                        try
                        {
                            imovel.Desconto = GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[1]/h5[2]/strong[1]", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim();
                            imovel.Valor = GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[1]/h5[2]/strong[2]", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim();
                        }
                        catch (Exception)
                        {
                            imovel.Valor = imovel.Desconto;
                            imovel.Desconto = "0";
                        }

                        imovel.Dormitorio = int.Parse(GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[1]/ul/li[1]", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim());
                        imovel.Tamanho = GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[1]/ul/li[3]", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim();
                        imovel.Vaga = int.Parse(GetHtmlFromWeb(site).DocumentNode.SelectNodes(string.Format("//*[@id='{0}']/a/div/div[1]/ul/li[2]", item.InnerText.Substring(item.InnerText.IndexOf(" ") + 1))).FirstOrDefault().InnerText.Trim());

                    }
                    catch (Exception)
                    {
                        Console.Clear();
                        Console.WriteLine(string.Format("Confirme as Informações do Imovel {0} codigo {1}", imovel.Categoria, imovel.Codigo));
                        Console.WriteLine("Favor Confirmar informações");
                        //bairro
                        Console.WriteLine("Imovel {0} Bairro: {1} está correto?",imovel.Codigo,imovel.Bairro);
                        Console.WriteLine("S / N");
                        string r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string info = Console.ReadLine();
                            imovel.Bairro = ConvertEncoding(info);                           
                        }
                        Console.WriteLine("Imovel {0} Categoria: {1} está correto?", imovel.Codigo, imovel.Categoria);
                        Console.WriteLine("S / N");
                        r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string info = Console.ReadLine().ToUpper();
                            imovel.Categoria = ConvertEncoding(info);
                        }
                        Console.WriteLine("Imovel {0} Valor de Desconto: {1} está correto?", imovel.Codigo, imovel.Desconto);
                        Console.WriteLine("S / N");
                        r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string info = Console.ReadLine().ToUpper();
                            imovel.Desconto = ConvertEncoding(info);
                        }
                        Console.WriteLine("Imovel {0} Valor: {1} está correto?", imovel.Codigo, imovel.Valor);
                        Console.WriteLine("S / N");
                        r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string info = Console.ReadLine().ToUpper();
                            imovel.Valor = ConvertEncoding(info);
                        }
                        Console.WriteLine("Imovel {0} Dormitorio: {1} está correto?", imovel.Codigo, imovel.Dormitorio);
                        Console.WriteLine("S / N");
                        r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string info = Console.ReadLine().ToUpper();
                            imovel.Dormitorio =int.Parse(info);
                        }
                        Console.WriteLine("Imovel {0} Tamanho: {1} está correto?", imovel.Codigo, imovel.Tamanho);
                        Console.WriteLine("S / N");
                        r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string infoTamanho = Console.ReadLine().ToUpper();
                            imovel.Tamanho = infoTamanho;
                        }
                        Console.WriteLine("Imovel {0} Vaga: {1} está correto?", imovel.Codigo, imovel.Vaga);
                        Console.WriteLine("S / N");
                        r = Console.ReadLine().ToUpper();
                        if (r == "N")
                        {
                            Console.WriteLine("Informe a informação correta");
                            string info = Console.ReadLine().ToUpper();
                            imovel.Vaga = int.Parse(info);
                        }
                        Console.Clear();
                        Console.WriteLine("informaçõs atualizadas");                        
                        Console.WriteLine("Continuando coleta de dados");

                    }
                    Imoveis.Add(imovel);
                }
            }
            Console.Clear();
            Console.WriteLine("Coletados {0} Imoveis", Imoveis.Count);
            Console.WriteLine("aperta qualquer tecla para continuar");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Informe o caminho que deseja salvar o arquivo de saida");
            Console.WriteLine(@"EXEMPLO: C:\Users\JP\Documents");
            string filesaida = Console.ReadLine();
            CriaExcel(filesaida, Imoveis);

        }

        public  static void CriaExcel(string caminho,List<Imovel> imoveis)
        {
            
            Excel excel = new Excel();
            DataTable dt = new DataTable(string.Format("Imoveis"));
            dt.Columns.Add("ID_Imovel");
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Bairro");
            dt.Columns.Add("Categoria");
            dt.Columns.Add("Desconto");
            dt.Columns.Add("Valor");
            dt.Columns.Add("Dormitorio");
            dt.Columns.Add("Tamanho");
            dt.Columns.Add("Vaga");
            foreach (var item in imoveis)
            {
                dt.Rows.Add(item.id_imovel, item.Codigo, item.Bairro, item.Categoria, item.Desconto, item.Valor, item.Dormitorio, item.Tamanho, item.Vaga);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            excel.WriteXLSX(ds, string.Format(caminho+ "/{0}_{1}.xlsx", "Imoveis",DateTime.Today.ToShortDateString().Replace('/','-')));
        }
        public static HtmlDocument GetHtmlFromWeb(string adress)
        {
            var html = adress;

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);
            return htmlDoc;
        }
        public static string ConvertEncoding(string textCagado)
        {
            Troca t = new Troca();
            return t.RetornaFraseSemASCII(textCagado);
        }



    }
}
