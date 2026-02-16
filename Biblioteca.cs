using System;
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

                    Console.WriteLine("Libro prestado a usuario " + idUsuario);
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

                    Console.WriteLine("Libro devuelto");
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
            foreach (Libro l in Libros)
            {
                Console.WriteLine(l.Titulo + " - " + l.ISBN + " - Disponible: " + l.Disponible);
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



            b.MostrarLibros();


            b.PrestarLibro("978-6070935299", 1);

            b.DevolverLibro("978-6070935299");



            Console.WriteLine("Ejecute el programa otra vez y verá que el libro sigue guardado");


            Console.ReadLine();

        }
    }
  
