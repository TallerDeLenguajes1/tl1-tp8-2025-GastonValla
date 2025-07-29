// See https://aka.ms/new-console-template for more information
// TP8 - Ejercicio 2: Calculadora con historial

using System;
using System.Collections.Generic;

namespace EspacioCalculadora {

    public enum TipoOperacion {
        Suma,
        Resta,
        Multiplicacion,
        Division,
        Limpiar
    }

    public class Operacion {
        private double resultadoAnterior;
        private double nuevoValor;
        private TipoOperacion operacion;

        public double Resultado {
            get {
                return operacion switch {
                    TipoOperacion.Suma => resultadoAnterior + nuevoValor,
                    TipoOperacion.Resta => resultadoAnterior - nuevoValor,
                    TipoOperacion.Multiplicacion => resultadoAnterior * nuevoValor,
                    TipoOperacion.Division => nuevoValor != 0 ? resultadoAnterior / nuevoValor : double.NaN,
                    TipoOperacion.Limpiar => 0,
                    _ => double.NaN
                };
            }
        }

        public double NuevoValor => nuevoValor;
        public TipoOperacion Tipo => operacion;
        public double Anterior => resultadoAnterior;

        public Operacion(double anterior, double nuevo, TipoOperacion tipo) {
            resultadoAnterior = anterior;
            nuevoValor = nuevo;
            operacion = tipo;
        }

        public override string ToString() {
            string simbolo = operacion switch {
                TipoOperacion.Suma => "+",
                TipoOperacion.Resta => "-",
                TipoOperacion.Multiplicacion => "*",
                TipoOperacion.Division => "/",
                TipoOperacion.Limpiar => "LIMPIAR",
                _ => "?"
            };

            if (operacion == TipoOperacion.Limpiar)
                return "Se limpió el resultado.";

            return $"{Anterior} {simbolo} {NuevoValor} = {Resultado}";
        }
    }

    public class Calculadora {
        private double dato;
        private List<Operacion> historial = new List<Operacion>();

        public void Sumar(double termino) {
            historial.Add(new Operacion(dato, termino, TipoOperacion.Suma));
            dato += termino;
        }

        public void Restar(double termino) {
            historial.Add(new Operacion(dato, termino, TipoOperacion.Resta));
            dato -= termino;
        }

        public void Multiplicar(double termino) {
            historial.Add(new Operacion(dato, termino, TipoOperacion.Multiplicacion));
            dato *= termino;
        }

        public void Dividir(double termino) {
            if (termino != 0) {
                historial.Add(new Operacion(dato, termino, TipoOperacion.Division));
                dato /= termino;
            } else {
                Console.WriteLine("Error. No divida por cero.");
            }
        }

        public void Limpiar() {
            historial.Add(new Operacion(dato, 0, TipoOperacion.Limpiar));
            dato = 0;
        }

        public double Resultado => dato;

        public void MostrarHistorial() {
            Console.WriteLine("--- Historial de operaciones ---");
            if (historial.Count == 0) Console.WriteLine("(vacío)");
            foreach (var op in historial) Console.WriteLine(op);
        }
    }

    class Program {
        static void Main(string[] args) {
            Calculadora calc = new Calculadora();
            string comando = "";

            while (comando != "salir") {
                Console.WriteLine($"\nResultado actual: {calc.Resultado:N2}");
                Console.WriteLine("Operaciones disponibles: sumar, restar, multiplicar, dividir, limpiar, historial, salir");
                Console.Write("Ingrese comando: ");
                comando = Console.ReadLine().ToLower();

                if (comando == "limpiar") calc.Limpiar();
                else if (comando == "historial") calc.MostrarHistorial();
                else if (comando == "salir") Console.WriteLine("Saliendo...");
                else if (comando == "sumar" || comando == "restar" || comando == "multiplicar" || comando == "dividir") {
                    Console.Write("Ingrese un número: ");
                    if (double.TryParse(Console.ReadLine(), out double valor)) {
                        switch (comando) {
                            case "sumar": calc.Sumar(valor); break;
                            case "restar": calc.Restar(valor); break;
                            case "multiplicar": calc.Multiplicar(valor); break;
                            case "dividir": calc.Dividir(valor); break;
                        }
                    } else {
                        Console.WriteLine("Error. Número inválido.");
                    }
                } else {
                    Console.WriteLine("Comando no reconocido.");
                }
            }
        }
    }
}

