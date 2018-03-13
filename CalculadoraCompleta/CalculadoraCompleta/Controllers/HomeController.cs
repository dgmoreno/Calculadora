using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {
        //esta solução não funciona
        //string primeiroOperando = "";


        // GET: Home
        public ActionResult Index(){
            //preparar os primeiros valores da calculadora
            ViewBag.Visor = 0;
            Session["operadorAnterior"] = "";
            Session["limpaVisor"] = true;


            return View();
        }

        //POST : Home
        [HttpPost]
        public ActionResult Index(string bt, string visor){


            //determinar a ação a executar
            switch (bt) {
                case "7":
                case "8":
                case "9":
                case "4":
                case "5":
                case "6":
                case "1":
                case "2":
                case "3":
                case "0":
                    //recupera o resultado da decisão sobre a limpeza do visor
                    bool limpaEcran= (bool)Session["limpaVisor"];
                    if (visor.Equals("0") || limpaEcran ) visor = bt;
                    else visor += bt;
                    //marcar o visor para continuar a escrita do operando
                    Session["limpaVisor"] = false;
                    break;
                case "+/-":
                    visor = Convert.ToDouble(visor) * -1 + "";
                    break;
                case ",":
                    if (!visor.Contains(",")) visor += bt;
                    break;
                case "+":
                case "-":
                case "x":
                case ":":
                case "=":
                    //se não é a primeira vez que pressiono um operadorAnterior
                    if (!((string)Session["operadorAnterior"]).Equals(""))
                    {

                        // agora é que se vai fazer a 'conta'
                        //obter os operandos
                        double primeiroOperando = Convert.ToDouble((string)Session["primeiroOperando"]);
                        double segundoOperando = Convert.ToDouble(visor);
                        //escolher a operação a fazer com operadorAnterior anterior
                        switch ((string)Session["operadorAnterior"]){
                            case "+":
                                visor = primeiroOperando + segundoOperando + "";
                                break;
                            case "-":
                                visor = primeiroOperando - segundoOperando + "";
                                break;
                            case "x":
                                visor = primeiroOperando * segundoOperando + "";
                                break;
                            case ":":
                                visor = primeiroOperando / segundoOperando + "";
                                break;
                        }//((string)Session["operadorAnterior"])
                    }
                    //preservar o valor do VISOR
                    //usando variáveis de sessão
                    //Sesseion permite preservar o valor enquanto a maquina cliente estiver ligada
                    //preservar os valores fornecidos para operações futuras
                    if(bt.Equals("=")) Session["operadorAnterior"] = "";
                    else Session["operadorAnterior"] = bt;

                        Session["primeiroOperando"] = visor;

                        //marcar o visor para 'limpeza'
                        Session["limpaVisor"] = true;
                    
                    break;
                case "c":
                    //vamos limpar a calculadora
                    //isto é, fazer um 'reset' total
                    visor = "0";
                    Session["operadorAnterior"] = "";
                    Session["limpaVisor"] = true;
                    break;
            }

            //enviar o resultado para a View
            ViewBag.Visor = visor;

            

            return View();
        }
    }
}