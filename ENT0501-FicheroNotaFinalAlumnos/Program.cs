using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NSFunciones;
using Evaluador;

namespace ENT0501_Notas_Alumnos
{
    class Program
    {
        static void Main(string[] args)
        {
            string path1 = @"C:\Users\Alumno\Documents\Datos Notas.csv";
            if (Fichero.ExisteFichero(path1) == false)      // 1.CONTROL DE ERROR: Si el fichero no existe, sale del programa.
            {
                Console.WriteLine("El fichero de texto necesario no existe. Se cerrará el programa.");
            }
            else
            {
                //2. Control de errores en manejo de fichero aplicados.
                if (Fichero.MostrarFichero(path1) == true)
                {
                    Console.WriteLine("Este es el fichero origianl:\n");
                    string[] AlumnosImpares = Fichero.ExtraerCadenaImpar(path1);        //Array donde guardo las lineas del fichero

                    if (Control.ErrorNotas(AlumnosImpares) == false)    //Función que comprueba errores en el tipo de datos de las notas y que su rango sea de 0 a 10
                    {
                        string[] NombresImpares = new string[AlumnosImpares.Length];        //Array donde se almacenan los Nombres
                        int[] NotasImpares = new int[AlumnosImpares.Length];                //Vector para las notas
                        string[] CadenaPartida = null;                                      //Array donde almacenar los string.split

                        for (int cont = 0; cont < AlumnosImpares.Length; cont++)
                        {
                            int sumaExa = 0;
                            int sumaEnt = 0;
                            int sumaInt = 0;
                            decimal mediaExa = 0;
                            decimal mediaEnt = 0;
                            decimal mediaInt = 0;
                            decimal mediaTotal = 0;

                            //Console.WriteLine(AlumnosImpares[cont]);  //Descomentar para comprobación.
                            CadenaPartida = AlumnosImpares[cont].Split(';');
                            NombresImpares[cont] = CadenaPartida[0];        //Almacenamos los nombres, que están en la posición 0 del split
                            for (int contsuma = 1; contsuma < CadenaPartida.Length; contsuma++) //Recorremos el array generado por el split a partir de la posición 1 (donde empiezan los números).
                            {
                                int nota = Convert.ToInt32(CadenaPartida[contsuma]);
                                /*Convertimos la nota en string a entero ya que previamente
                                hemos comprobado su tipo de dato con la función Control.ErrorNotas.
                                */

                                if (contsuma <= 3)       //Notas de entregas
                                {
                                    sumaEnt = nota + sumaEnt;
                                }
                                else
                                {
                                    if (contsuma <= 6)      //Notas de intervenciones
                                    {
                                        sumaInt = nota + sumaInt;
                                    }
                                    else
                                    {
                                        if (contsuma <= 8)   //Notas de examenes
                                        {
                                            sumaExa = nota + sumaExa;
                                        }
                                    }
                                }
                                mediaExa = Convert.ToDecimal((sumaExa / 2.00) * 0.50);     // Ponemos los divisores como decimales para que la operación devuelva los valores correctos.
                                mediaInt = Convert.ToDecimal((sumaInt / 3.00) * 0.20);
                                mediaEnt = Convert.ToDecimal((sumaEnt / 3.00) * 0.30);
                                mediaTotal = mediaExa + mediaEnt + mediaInt;
                                NotasImpares[cont] = Convert.ToInt32(Math.Floor(mediaTotal));
                            }
                        }
                        /*
                        //Descomentar para comprobación en consola
                        for (int cont = 0; cont < NombresImpares.Length; cont++)
                        {
                            Console.WriteLine(NombresImpares[cont] + NotasImpares[cont]);
                        }
                        Console.ReadKey();
                        */
                        string path2 = @"C:\Users\Alumno\Documents\salida.txt";
                        if (Fichero.ExisteFichero(path2) == true)   //Si el fichero de salida ya existía, lo sobreescribimos:
                        {
                            Console.WriteLine("El fichero ya existe, será sobreescrito.");
                            Console.WriteLine("Pulse una tecla para continuar...");
                            Console.ReadKey();

                            if (Fichero.EscribirNotas(NombresImpares, NotasImpares, path2) == false)
                            {
                                Console.WriteLine("Error en el manejo del fichero {0}", path2); //Si la función nos devuelve un falso, es que hubo un error en el manejo.
                            }    
                        }
                        else
                        {
                            if (Fichero.EscribirNotas(NombresImpares, NotasImpares, path2) == false)
                            {
                                Console.WriteLine("Error en el manejo del fichero {0}", path2); //Si la función nos devuelve un falso, es que hubo un error en el manejo.
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Datos y/o valores del fichero incorrectos. Se procede a salir del programa.");
                    }
                }
                else
                {
                    Console.WriteLine("Error en el manejo del fichero {0}", path1); //Si la función MostrarFichero nos ha devuelto un false, ha ocurrido un error en el manejo del fichero.
                }
            }
            Funciones.Finprograma();        //Nota: Esta función incluye un Console.ReadKey() para pausar la finalización.
        }
    }
}