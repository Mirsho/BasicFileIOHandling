using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NSFunciones;

namespace Evaluador
{
    class Control
    {
        public static bool CeroDiez(int nota)
        {
            bool correcto = true;
            if (nota > 10)      //Si la nota es mayor o igual que 0 pasa a comprobar que sea menor
            {
                correcto = false;
            }
            else
            {
                if (nota < 0)      //si la nota es menor o igual a 10 continua
                {
                    correcto = false;
                }
            }
            return(correcto);
        } 

        public static bool ErrorNotas(string[] lista)   //le pasamos el array con la lista de notas.
        {
            bool final = false;
            bool error = false;
            int cont = 0;   //Empezamos el contador en 1 ya que queremos obviar los nombres ubicados en la posición 0.
            string[] cadenaPartida = null;
            do
            {
                bool salida = false;
                int contlinea = 1;
                cadenaPartida = lista[cont].Split(';'); //hacemos un split a cada array de la lista.
                do
                {
                    if (Int32.TryParse(cadenaPartida[contlinea], out int nota) == true)      //Si el valor tratado se puede convertir a entero, lo devolvemos como la variable Nota. ATENCIÓN: TryParse devuelve 
                    {                                                                       //la variable out como 0 si no la puede convertir, por eso entramos al if si ha devuelto true (lo ha podido convertir).
                        if (Control.CeroDiez(nota) == false)
                        {
                            error = true;   //Si no está entre 0 y 10.
                            salida = true;
                        }
                        else
                        {
                            if (contlinea < (cadenaPartida.Length - 1))   //Si contlinea es menor que array.Lenght, sumamos 1 a contlinea y volvemos
                            {
                                contlinea++;
                            }
                            else
                            {
                                salida = true;  //Si contlinea es igual o mayor que array.Length, ponemos salida a true y salimos del bucle.
                            }
                        }
                    }
                    else
                    {
                        error = true;   //si se ha detectado un error en el tipo de dato, salida se pone a true y salimos del bucle
                        salida = true;
                    }
                } while (salida == false);

                if (error == true)
                {
                    final = true;       //Si se ha registrado un error, salimos del bucle
                }
                else
                {
                    if (cont < (lista.Length - 1))
                    {
                        cont++; //Si no hemos superado a la posición final del array, sumamos cont y seguimos.
                    }
                    else
                    {
                        final = true;   //cuando hemos superado la posición final, terminamos.
                    }
                }
            } while (final == false);
            return (error);
        }
    }
}
