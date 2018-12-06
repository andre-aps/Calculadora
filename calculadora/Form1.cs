using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculadora
{
    public partial class frmCalc : Form
    {
        double valor = 0; //Variável que será usada para armazenar o resultado dos cálculos
        string operacao = ""; //Variável que será usada para obter o Text dos operadores
        bool clique_operacao = false; //Saber se algum operador foi acionado 

        public frmCalc()
        {
            InitializeComponent();
        }

        private void numero_Click(object sender, EventArgs e)
        {
            //Caso a text seja igual a '0' ou algum operador for acionado, a text será limpa para que um novo valor possa ser adicionado à text
            if ((txtResultado.Text == "0") || (clique_operacao)) {
                txtResultado.Clear();
            }

            //Se o tamanho for maior que 13, a text será limpa
            if (txtResultado.Text.Length > 13) {
                txtResultado.Clear();
            }

            //Caso a label contenha o texto abaixo, a mesma será limpa no próximo clique de algum número
            if(lblHistorico.Text.Contains("Não é possível dividir por 0")) {
                lblHistorico.Text = "";
            }

            clique_operacao = false; //Necessário para que possa ser adicionados vários números na text 
            Button b = (Button)sender; //Variável do tipo button com o parâmetro sender
            txtResultado.Text += b.Text; //A text exibe o valor já digitado + o texto do botão clicado 
        }

        private void operacao_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            operacao = b.Text; //Operacao recebe o text do operador          
            valor = Convert.ToDouble(txtResultado.Text); //Converte a text em um número para poder calcular          
            clique_operacao = true; //Para que se possa adicionar um novo valor para calcular
            lblHistorico.Text = valor + " " + operacao; //A label exibe o valor da text junto com o sinal do operador            
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {            
            lblHistorico.Text = "";            
            switch (operacao)
            {
                //Dependendo do operador, é realizado uma conversão na text e o cálculo é feito junto à variável valor. Depois é feito uma conversão para string.                              
                case "+":                    
                    txtResultado.Text = (valor + Convert.ToDouble(txtResultado.Text)).ToString();                    
                    break;
                case "-":
                    txtResultado.Text = (valor - Convert.ToDouble(txtResultado.Text)).ToString();
                    break;
                case "x":
                    txtResultado.Text = (valor * Convert.ToDouble(txtResultado.Text)).ToString();
                    break;
                case "÷":
                    txtResultado.Text = Math.Round((valor / Convert.ToDouble(txtResultado.Text)), 10).ToString();
                    if(txtResultado.Text.Contains("∞") || txtResultado.Text.Contains("NaN")) {
                        txtResultado.Text = "0";
                        lblHistorico.Text = "Não é possível dividir por 0";
                    }
                    break;                
                default:
                    break;
            }            
        }

        private void btnCE_Click(object sender, EventArgs e)
        {            
            txtResultado.Text="0";
        }        

        private void btnVirgula_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if(!txtResultado.Text.Contains(",")) { //Se a text não conter ',' será adicionado o texto do botão à text
                txtResultado.Text += b.Text;                
            }
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            //Para que os caracteres do texto abaixo não sejam apagados um a um, a text retorna para o valor 0
            if(txtResultado.Text.Contains("NaN")) {
                txtResultado.Text = "0";
            }

            //Método remove passando como parâmetro o comprimento da text e excluindo um do mesmo
            txtResultado.Text = txtResultado.Text.Remove(txtResultado.Text.Length - 1);        

            if (txtResultado.Text == "" || txtResultado.Text == "-")
            {
                txtResultado.Text = "0";
            }
        }

        private void btnInverte_Click(object sender, EventArgs e)
        {
            //Inverte o número da text para negativo ou positivo multiplicando-o por -1
            double num;
            num = Convert.ToDouble(txtResultado.Text);
            num *= -1;
            txtResultado.Text = num.ToString();
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            //Limpa a text, a variável valor e a label que contém o histórico
            txtResultado.Text = "0";
            valor = 0;
            lblHistorico.Text = "";
        }        
    }
}
