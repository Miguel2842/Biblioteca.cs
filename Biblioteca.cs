using System.Collections.Generic;
using System.IO;

namespace BibliotecaSimple
{

    public class Libro
    {
        public string Titulo;
        public string ISBN;
        public bool Disponible = true;

        public void Prestar() { Disponible = false; }
        public void Devolver() { Disponible = true; }
    }



    public class Usuario
    {
        public int Id;
        public string Nombre;

        public Usuario(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }



    public class Biblioteca
    {
        public List<Libro> Libros = new List<Libro>();
        public List<Usuario> Usuarios = new List<Usuario>();

        string archivo = "libros.txt";


        public void AgregarLibro(Libro l)
        {
            Libros.Add(l);
            GuardarLibros();
        }


        public void AgregarUsuario(Usuario u)
        {
            Usuarios.Add(u);
        }



        public void PrestarLibro(string isbn, int idUsuario)
        {
            foreach (Libro l in Libros)
            {
                if (l.ISBN == isbn && l.Disponible)
                {
                    l.Prestar();

                    GuardarLibros();

                    Console.WriteLine("Libro prestado correctamente");
                    return;
                }
            }

            Console.WriteLine("No se pudo prestar el libro");
        }



        public void DevolverLibro(string isbn)
        {
            foreach (Libro l in Libros)
            {
                if (l.ISBN == isbn && !l.Disponible)
                {
                    l.Devolver();

                    GuardarLibros();

                    Console.WriteLine("Libro devuelto correctamente");
                    return;
                }
            }

            Console.WriteLine("No se encontró el libro");
        }



        public void GuardarLibros()
        {
            StreamWriter writer = new StreamWriter(archivo);

            foreach (Libro l in Libros)
            {
                writer.WriteLine(
                    l.Titulo + "," +
                    l.ISBN + "," +
                    l.Disponible
                );
            }

            writer.Close();
        }



        public void CargarLibros()
        {
            if (File.Exists(archivo))
            {
                StreamReader reader = new StreamReader(archivo);

                string linea;

                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');

                    Libro l = new Libro();

                    l.Titulo = datos[0];
                    l.ISBN = datos[1];
                    l.Disponible = bool.Parse(datos[2]);

                    Libros.Add(l);
                }

                reader.Close();
            }
        }



        public void MostrarLibros()
        {
            Console.WriteLine("\nLISTA DE LIBROS:");

            foreach (Libro l in Libros)
            {
                Console.WriteLine(
                    "Titulo: " + l.Titulo +
                    " | ISBN: " + l.ISBN +
                    " | Disponible: " + l.Disponible
                );
            }
        }

    }




    class Program
    {
        static void Main(string[] args)
        {

            Biblioteca b = new Biblioteca();

            b.CargarLibros();

            Usuario usuario1 = new Usuario(1, "Miguel");

            b.AgregarUsuario(usuario1);


            if (b.Libros.Count == 0)
            {
                Libro libro1 = new Libro
                {
                    Titulo = "El Principe",
                    ISBN = "978-6070935299"
                };

                b.AgregarLibro(libro1);
            }



            int opcion;

            do
            {
                Console.WriteLine("\n=== SISTEMA DE BIBLIOTECA ===");
                Console.WriteLine("1. Mostrar libros");
                Console.WriteLine("2. Prestar libro");
                Console.WriteLine("3. Devolver libro");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opcion: ");

                opcion = int.Parse(Console.ReadLine());


                if (opcion == 1)
                {
                    b.MostrarLibros();
                }
                else if (opcion == 2)
                {
                    Console.Write("Ingrese ISBN del libro: ");

                    string isbn = Console.ReadLine();

                    b.PrestarLibro(isbn, 1);
                }
                else if (opcion == 3)
                {
                    Console.Write("Ingrese ISBN del libro: ");

                    string isbn = Console.ReadLine();

                    b.DevolverLibro(isbn);
                }

            }

            while (opcion != 4);



            Console.WriteLine("Programa finalizado");

            Console.ReadLine();

        }
    }
}


  
