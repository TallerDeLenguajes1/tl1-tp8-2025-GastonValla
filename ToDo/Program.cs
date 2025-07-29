// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World! \n \n");
// Programa principal del Módulo ToDo (TP8 - Punto 1)

using System;
using System.Collections.Generic;

namespace ToDoApp {
    public class Tarea {
        public int TareaID { get; set; }
        public string Descripcion { get; set; }
        public int Duracion {
            get => duracion;
            set {
                if (value >= 10 && value <= 100)
                    duracion = value;
                else
                    duracion = 10; // Valor por defecto si es inválido
            }
        }

        private int duracion;

        public Tarea(int id, string descripcion, int duracion) {
            TareaID = id;
            Descripcion = descripcion;
            Duracion = duracion;
        }

        public override string ToString() {
            return $"ID: {TareaID} | Descripción: {Descripcion} | Duración: {Duracion} min";
        }
    }

    class Program {
        static List<Tarea> tareasPendientes = new List<Tarea>();
        static List<Tarea> tareasRealizadas = new List<Tarea>();
        static int contadorID = 1;
        static Random rand = new Random();

        static void Main(string[] args) {
            int opcion;
            do {
                MostrarMenu();
                opcion = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (opcion) {
                    case 1:
                        CrearTareas();
                        break;
                    case 2:
                        MoverTareaRealizada();
                        break;
                    case 3:
                        BuscarPorDescripcion();
                        break;
                    case 4:
                        MostrarTodasLasTareas();
                        break;
                    case 0:
                        Console.WriteLine("Saliendo del programa...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Console.WriteLine();
            } while (opcion != 0);
        }

        static void MostrarMenu() {
            Console.WriteLine("--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Crear N tareas aleatorias");
            Console.WriteLine("2. Mover tarea de pendientes a realizadas");
            Console.WriteLine("3. Buscar tarea pendiente por descripción");
            Console.WriteLine("4. Mostrar todas las tareas");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");
        }

        static void CrearTareas() {
            Console.Write("Ingrese la cantidad de tareas a crear: ");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i++) {
                string descripcion = "Tarea " + contadorID;
                int duracion = rand.Next(10, 101);
                tareasPendientes.Add(new Tarea(contadorID++, descripcion, duracion));
            }
            Console.WriteLine($"Se crearon {n} tareas pendientes.");
        }

        static void MoverTareaRealizada() {
            Console.WriteLine("Ingrese el ID de la tarea a marcar como realizada: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Tarea? tarea = tareasPendientes.Find(t => t.TareaID == id);

            if (tarea != null) {
                tareasPendientes.Remove(tarea);
                tareasRealizadas.Add(tarea);
                Console.WriteLine("Tarea movida a realizadas.");
            } else {
                Console.WriteLine("No se encontró una tarea con ese ID en pendientes.");
            }
        }

        static void BuscarPorDescripcion() {
            Console.Write("Ingrese texto para buscar en descripciones: ");
            string texto = Console.ReadLine().ToLower();

            var resultados = tareasPendientes.FindAll(t => t.Descripcion.ToLower().Contains(texto));

            if (resultados.Count > 0) {
                Console.WriteLine("Tareas encontradas:");
                foreach (var tarea in resultados) {
                    Console.WriteLine(tarea);
                }
            } else {
                Console.WriteLine("No se encontraron tareas con esa descripción.");
            }
        }

        static void MostrarTodasLasTareas() {
            Console.WriteLine("--- TAREAS PENDIENTES ---");
            if (tareasPendientes.Count == 0)
                Console.WriteLine("(ninguna)");
            foreach (var t in tareasPendientes)
                Console.WriteLine(t);

            Console.WriteLine("\n--- TAREAS REALIZADAS ---");
            if (tareasRealizadas.Count == 0)
                Console.WriteLine("(ninguna)");
            foreach (var t in tareasRealizadas)
                Console.WriteLine(t);
        }
    }
}
