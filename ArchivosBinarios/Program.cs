using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchivosBinarios
{
    class ArchivosBinariosEmpleados
    {
        //declaracion de flujos
        BinaryWriter bw = null; //flujo salida - escritura de datos
        BinaryReader br = null; //flujo entrada - lectura de datos

        //campos de la clase
        string nombre, direccion;
        long telefono;
        int numEmp, diasTrabajados;
        float salarioDiario;

        public void CrearArchivo(string archivo)
        {
            //variable local método
            char resp;

            try
            {
                //creación del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(archivo, FileMode.Create, FileAccess.Write));

                //captura de datos
                do
                {
                    Console.Clear();
                    Console.Write("Numero del Empleado: ");
                    numEmp = Int32.Parse(Console.ReadLine());
                    Console.Write("Nombre del Empleado: ");
                    nombre = Console.ReadLine();
                    Console.Write("Direccion del Empleado: ");
                    direccion = Console.ReadLine();
                    Console.Write("Telefono del Empleado: ");
                    telefono = Int64.Parse(Console.ReadLine());
                    Console.Write("Dias Trabajados del Empleado: ");
                    diasTrabajados = Int32.Parse(Console.ReadLine());
                    Console.Write("Salario Diario del Empleado: ");
                    salarioDiario = Single.Parse(Console.ReadLine());

                    //escribe los datos al archivo
                    bw.Write(numEmp);
                    bw.Write(nombre);
                    bw.Write(direccion);
                    bw.Write(telefono);
                    bw.Write(diasTrabajados);
                    bw.Write(salarioDiario);

                    Console.Write("\n\nDesea Almacenar otro Registro (s/n)?: ");
                    resp = char.Parse(Console.ReadLine());
                }
                while (resp == 's' || resp == 'S');
            }
            catch (Exception e)
            {
                Console.WriteLine("\nError: " + e.Message);
                Console.WriteLine("\nRuta: " + e.StackTrace);
            }
            finally
            {
                if (bw != null) bw.Close(); //cierra el flujo - escritura

                Console.WriteLine("\nPresione <enter> para terminar la Escritura de Datos y Regresar al Menu.");
                Console.ReadKey();
            }
        }

        public void MostrarArchivo(string archivo)
        {
            try
            {
                //verifica si existe el archivo
                if (File.Exists(archivo))
                {
                    //creación flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(archivo, FileMode.Open, FileAccess.Read));

                    Console.Clear();

                    //despliegue de datos en pantalla
                    do
                    {
                        //lectura de registros mientras no llegue a EndOfFile
                        numEmp = br.ReadInt32();
                        nombre = br.ReadString();
                        direccion = br.ReadString();
                        telefono = br.ReadInt64();
                        diasTrabajados = br.ReadInt32();
                        salarioDiario = br.ReadSingle();

                        //muestra los datos
                        Console.WriteLine("Numero del Empleado: " + numEmp);
                        Console.WriteLine("Nombre del Empleado: " + nombre);
                        Console.WriteLine("Direccion del Empleado: " + direccion);
                        Console.WriteLine("Telefono del Empleado: " + telefono);
                        Console.WriteLine("Dias Trabajados del Empleado: " + diasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado: {0:C}", salarioDiario);

                        Console.WriteLine("Sueldo Total del Empleado: {0:C}", diasTrabajados * salarioDiario);
                        Console.WriteLine("\n");
                    }
                    while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\nEl Archivo " + archivo + " no Existe en el Disco!");
                    Console.Write("\nPresione <enter> para Continuar . . .");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados.");
                Console.Write("\nPresione <enter> para Continuar . . .");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //cierra flujo

                Console.Write("\nPresione <enter> para terminar la Lectura de Datos y Regresar al Menu.");
                Console.ReadKey();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //declaración variables auxiliares
            string arch = null;
            int opcion;

            //creación del objeto
            ArchivosBinariosEmpleados a1 = new ArchivosBinariosEmpleados();

            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creación de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opción deseas: ");
                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        //bloque de escritura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");

                            arch = Console.ReadLine();

                            //verifica si esxiste el archivo
                            char resp = 's';
                            if (File.Exists(arch))
                            {
                                Console.Write("\nEl Archivo Existe!!, Deseas Sobreescribirlo(s / n) ? ");

                                resp = Char.Parse(Console.ReadLine());
                            }
                            if ((resp == 's') || (resp == 'S'))
                                a1.CrearArchivo(arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 2:
                        //bloque de lectura
                        try
                        {
                            //captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: ");

                            arch = Console.ReadLine();
                            a1.MostrarArchivo(arch);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine("\nError : " + e.Message);
                            Console.WriteLine("\nRuta : " + e.StackTrace);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");

                        Console.ReadKey();
                        break;
                    default:
                        Console.Write("\nEsa Opción No Existe!!, Presione < enter > para Continuar...");
                        Console.ReadKey();
                        break;
                }
            }
            while (opcion != 3);
        }
    }
}