using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NSFunciones
{
    class Funciones
    {
        public static void Finprograma()
        {
            Console.Write("\n\nPulse una tecla para finalizar...");
            Console.ReadKey();
        }
    }

    class Fichero
    {
        public static StreamReader SRead = null;
        public static StreamWriter SWrite = null;

        public static bool InicioFichero(string fichero)
        {
            bool valor = true;
            if (!ExisteFichero(fichero))   //1. Si no existe el fichero...
            {
                //La función InicioFichero comprueba si el fichero existe o da errores al encontrarlo
                //Despues verificamos si da error al crearlo
                try
                {
                    SWrite = File.CreateText(fichero);      //2. Intentamos crear el fichero...
                    SWrite.Close();     //IMPORTANTE: Cerrar siempre el fichero despues de poseerlo en escritura.
                }
                catch (Exception ex)        //3. Capturando posibles errores al crearlo.
                {
                    Console.WriteLine(ex.Message);       //Muestra el mensaje de error que nos devuelve el sistema.
                    valor = false;
                }
            }
            return (valor);
        }

        public static bool ExisteFichero(string fichero)      //Comprueba si el fichero existe o no.
        {
            bool valor = true;
            try
            {
                SRead = new StreamReader(fichero);
                SRead.Close();      //VOLVEMOS A CERRAR el fichero
            }
            catch
            {
                valor = false;
            }
            return (valor);
        }

        public static bool CrearFichero(string fichero)
        {
            bool valor = true;
            try
            {
                SWrite = File.CreateText(fichero);
                SWrite.Close();
                Console.Clear();
            }
            catch (Exception ex)
            {
                //El proceso de creación dio error. Devolvemos el mensaje de sistema y salimos del programa.
                Console.WriteLine(ex.Message);
                valor = false;
            }
            return (valor);
        }

        public static bool EscribirFichero(string fichero, string cadena)       // 6.ABRE EL FICHERO, ESCRIBE 1 LINEA, CIERRA EL FICHERO.
        {                                                                       // ARREGLAR CON UN FOREACH. Creamos otra función que trabaja con arrays de string.
            bool valor = true;
            try
            {
                SWrite = new StreamWriter(fichero, true);
                Console.Clear();
                SWrite.WriteLine(cadena);
                SWrite.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                valor = false;
                Console.ReadKey();
            }
            return (valor);
        }

        public static bool EscribirFicheroArray(string fichero, string[] lista)
        { 
            bool valor = true;
            try
            {
                SWrite = new StreamWriter(fichero, true);
                Console.Clear();
                foreach (string cadena in lista)    //Con el foreach extraemos cada linea del array de string...
                {
                    SWrite.WriteLine(cadena);   //...y lo escribimos en el fichero...
                }
                SWrite.Close();     //...para finalmente cerrarlo.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                valor = false;
                Console.ReadKey();
            }
            return (valor);
        }

        public static string[] ExtraerCadenaImpar(string fichero)
        {
            string[] cadena = new string[0];
            int cont = 0;
            int linea = 1;
            try
            {
                SRead = new StreamReader(fichero, Encoding.Default);
                Console.Clear();
                while (!SRead.EndOfStream)
                {
                    if ((linea % 2) != 0)        //Solo extraemos cuando la línea es impar.
                    {
                        Array.Resize(ref cadena, cadena.Length + 1);    //Añadimos un espacio al array
                        cadena[cont] = (SRead.ReadLine());              //metemos la línea leida en la posición actual del array
                        cont++;                                         //Incrementamos la posición del array
                    }
                    else
                    {
                        SRead.ReadLine();       //Si no es impar, leemos linea para que el SReader avanze por el fichero.
                    }
                    linea++;                    //Siempre incrementamos el número de línea
                }
                SRead.Close();
            }
            catch (Exception ex)
            {
                //El proceso anterior a dado ERROR. Mostramos error de sistema y salimos del programa.
                Console.WriteLine(ex.Message);
            }
            return (cadena);
        }

        public static bool MostrarFichero(string fichero)
        {
            bool valor = true;
            try
            {
                SRead = new StreamReader(fichero, Encoding.Default);    //Codificamos la lectura en el idioma por defecto del S.O., hace uso de System.Text.
                Console.Clear();
                Console.WriteLine("Abriendo fichero de partida: ");
                Console.WriteLine("-----------------------------");
                while (!SRead.EndOfStream)
                {
                    Console.WriteLine(SRead.ReadLine());
                }
                SRead.Close();
                Console.WriteLine("\n\nPulse una tecla para continuar.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //El proceso anterior a dado ERROR. Mostramos error de sistema y salimos del programa.
                Console.WriteLine(ex.Message);
                valor = false;
            }
            return (valor);
        }

        public static bool EliminarFichero(string fichero) //Elimina una línea del fichero
        {
            bool valor = true;
            string[] lineas = new string[0];
            try
            {
                SRead = new StreamReader(fichero);
                Console.Clear();
                string linea = SRead.ReadLine();
                while (!SRead.EndOfStream)
                {
                    Array.Resize(ref lineas, lineas.Length + 1);
                    lineas[lineas.Length - 1] = SRead.ReadLine();
                }
                SRead.Close();
                SWrite = File.CreateText(fichero);
                foreach (string leida in lineas)
                {
                    SWrite.WriteLine(leida);
                }
                SWrite.Close();
                Console.Write("Se ha eliminado la línea: " + linea);
                Console.WriteLine("\n\nPulsa una tecla para continuar...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                //El proceso de elminiación ha dado error. Devolvemos el mensaje de error y salimos del programa
                Console.WriteLine(ex.Message);
                valor = false;
            }
            return (valor);
        }

        public static bool EscribirNotas(string[] nombres, int[] notas, string path)
        {
            bool valor = true;
            if (Fichero.CrearFichero(path) == true)
            {
                string[] notafinalalumno = new string[notas.Length + 1];   //Array de string donde concatenaremos los datos obtenidos
                for (int cont = 0; cont < nombres.Length; cont++)
                {
                    notafinalalumno[cont] = nombres[cont] + ";" + notas[cont];
                }
                if (Fichero.EscribirFicheroArray(path, notafinalalumno) == false)    //Pasamos el array de string con todos los datos a la función EscribirFicheroArray
                {
                    valor = false; //Si la función EscribirFicheroArray nos ha devuelto un false, ha ocurrido un error en el manejo del fichero.
                }
                else
                {
                    Console.WriteLine("Operacion completadas con éxito. \n\nPuede encontrar el archivo en: " + path);
                }
            }
            else
            {
                valor = false; //Si la función CrearFichero nos ha devuelto un false, ha ocurrido un error en el manejo del fichero.
            }
            return (valor);
        }
    }
}
