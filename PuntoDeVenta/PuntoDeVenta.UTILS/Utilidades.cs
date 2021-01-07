
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CMV.BancaAdmin.UTILS
{
    public static class Utilidades
    {
        public static bool ValidadContrasenaBanca(string contrasena, string contrasenaConfirmacion)
        {
            try
            {
                if (contrasena.Trim().Length < 8)
                    throw new Exception("La contraseña no cumple el criterio minimo de longitud, debe tener por lo menos 8 carácteres.");
                if (contrasena.Trim().Length > 50)
                    throw new Exception("La contraseña debe contener máximo 50 carácteres.");
                if (string.Compare(contrasena, contrasenaConfirmacion) != 0)
                    throw new Exception("Las contraseñas capturadas no coinciden.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public static string GenerarContrasenaAleatoria()
        {
            string contrasena = string.Empty;
            try
            {
                while (true)
                {
                    int longitud = 8;
                    string expresion = @"\A(^([a-zA-Z0-9]\1{2})){8,}";
                    string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                    string carateresEspeciales = "";
                    Random random = new Random();
                    while (0 < longitud--)
                    {
                        contrasena += caracteres[random.Next(caracteres.Length)];

                    }
                    Regex regex = new Regex(expresion);
                    if (regex.IsMatch(contrasena))
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return contrasena;
        }

        public static void MuestraErrores(string mensaje)
        {
            try
            {
                MessageBox.Show(mensaje, "“Las Cabañas”", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static void MuestraInfo(string mensaje)
        {
            try
            {
                MessageBox.Show(mensaje, "“Las Cabañas”", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static DialogResult MuestraPregunta(string mensaje)
        {
            try
            {
                return MessageBox.Show(mensaje, "“Las Cabañas”", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void MuestraAdvertencias(string mensaje)
        {
            try
            {
                MessageBox.Show(mensaje, "“Las Cabañas”", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public static bool ValidatePassword(string password, out string ErrorMessage, string numeroSocio)
        {
            try
            {
                var input = password;
                ErrorMessage = string.Empty;
                //al menos una Mayúscula, al menos una minúscula, al menos un Número, al menos un carácter especial
                // var validacion = new Regex(@"/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&-_#])([A-Za-z\d$@$!%*?&]|[^ ]){8,40}$/;");
                var valida_3_caracteres_iguales_consecutivos = new Regex(@"(.)\1{2,}");
                List<string> caja = new List<string>() { "CajaMoreliaValladolid", "Caja", "Banco", "morelia", "valladolid", "cmv" };

                var validacion_contiene_numeros = new Regex(@"[0-9]+");
                var validacion_cotiene_Mayus = new Regex(@"[A-Z]+");
                var valida_longitud = new Regex(@".{8,40}");
                var valida_contiene_Minus = new Regex(@"[a-z]+");
                var valida_caracter_especial = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

                //if (!validacion.IsMatch(input))
                //{
                //    ErrorMessage = "La contraseña no cumple con las politcas de seguridad.";
                //    return false;
                //}
                if (input.Length < 8)
                {
                    ErrorMessage = "La contraseña debe tener minimo 8 carácteres.";
                    return false;
                }
                if (!valida_longitud.IsMatch(input))
                {
                    ErrorMessage = "La contraseña no cumple con las politcas de seguridad.";
                    return false;
                }
                else if (!validacion_contiene_numeros.IsMatch(input))
                {
                    ErrorMessage = "La contraseña debe de contener al menos un número";
                    return false;
                }
                else if (!validacion_cotiene_Mayus.IsMatch(input))
                {
                    ErrorMessage = "La contraseña debe tener al menos una Mayuscula";
                    return false;
                }
                else if (!valida_contiene_Minus.IsMatch(input))
                {
                    ErrorMessage = "La contraseña debe tener al menos una Minuscula";
                    return false;
                }
                else if (!valida_caracter_especial.IsMatch(input))
                {
                    ErrorMessage = "La contraseña debe tener al menos un caracter especial";
                    return false;
                }
                else if (valida_3_caracteres_iguales_consecutivos.IsMatch(input.ToLower()))
                {
                    ErrorMessage = "La contraseña contiene 3 o mas letras o números consecutivos";
                    return false;
                }
                else if (!string.IsNullOrEmpty(caja.FirstOrDefault(x => input.ToLower().Contains(x.ToLower()))))
                {
                    ErrorMessage = "la contraseña no puede contener el nombre de la institución";
                    return false;
                }
                else if (input.Contains(Convert.ToString(numeroSocio)))
                {
                    ErrorMessage = "la contraseña no puede contener el número de socio";
                    return false;
                }
                else
                {
                    // Validamos las reglas de que la contraseña no debe tener 123 ,321 , abc ,cba
                    List<char> consecutivos = new List<char>();
                    int contadorSecuenciaNumeros = 0;
                    int contadorSecuenciaLetras = 0;

                    //validamos letras descendetes
                    for (int c = 0; c < input.Count() - 1; c++)
                    {

                        if (char.IsLetter(input[c]))
                        {
                            if ((Convert.ToInt16(input[c]) - 1) == Convert.ToInt16(input[c + 1]))
                                contadorSecuenciaLetras++;
                            else
                                contadorSecuenciaLetras = 0;
                        }

                        if (contadorSecuenciaLetras >= 2)
                        {
                            ErrorMessage = "La contraseña no puede contener carácteres alfabéticos consecutivos ejem(cba)";
                            return false;
                        }

                    }

                    contadorSecuenciaLetras = 0;
                    // validamos letras ascendentes
                    for (int c = 0; c < input.Count() - 1; c++)
                    {

                        if (char.IsLetter(input[c]))
                        {
                            if ((Convert.ToInt16(input[c]) + 1) == Convert.ToInt16(input[c + 1]))
                                contadorSecuenciaLetras++;
                            else
                                contadorSecuenciaLetras = 0;
                        }

                        if (contadorSecuenciaLetras >= 2)
                        {
                            ErrorMessage = "La contraseña no puede contener carácteres alfabéticos consecutivos ejem(abc)";
                            return false;
                        }
                    }
                    //validamos numeros 
                    for (int c = 0; c < input.Count(); c++)
                    {
                        if (char.IsDigit(input[c]))
                        {
                            consecutivos.Add(input[c]);
                            if (consecutivos.Count >= 3)
                            {
                                // validamos ascendentes
                                for (int i = 0; i < consecutivos.Count - 1; i++)
                                {
                                    if ((consecutivos[i] + 1) == consecutivos[i + 1])
                                    {
                                        contadorSecuenciaNumeros++;
                                    }
                                    else
                                        contadorSecuenciaNumeros = 0;
                                }
                                if (contadorSecuenciaNumeros >= 2)
                                {
                                    ErrorMessage = "La contraseña no puede contener números consecutivos ejem(123)";
                                    return false;
                                }
                                // validamos descendentes
                                for (int i = 0; i < consecutivos.Count - 1; i++)
                                {
                                    if ((consecutivos[i] - 1) == consecutivos[i + 1])
                                    {
                                        contadorSecuenciaNumeros++;
                                    }
                                    else
                                        contadorSecuenciaNumeros = 0;
                                }
                                if (contadorSecuenciaNumeros >= 2)
                                {
                                    ErrorMessage = "La contraseña no puede contener números consecutivos ejem(321)";
                                    return false;
                                }
                            }

                        }
                        else
                            consecutivos.Clear();

                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
    }
