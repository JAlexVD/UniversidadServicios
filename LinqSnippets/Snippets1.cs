using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public class Snippets1
    {

        public static void BaasicLinq()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat León"
            };

            //Select * from

            var carList = from car in cars select car;

            foreach (var car in carList)
            {
                Console.WriteLine(car);
            }

            var audiList = from car in cars where cars.Contains("Audi") select car;

            foreach (var audi in audiList)
            {
                Console.WriteLine(audi);
            }
        }


        //numeros
        public static void LinqNumbers()
        {
            List<int> numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            //Multiplica *3
            //Seleccionar menos el 9
            //Ordenar

            var listNumbers = numbers.
                                    Select(num => num * 3)
                                    .Select(num => num != 9)
                                    .OrderBy(num => num);
        }


        //
        public static void SearchExample()
        {
            List<string> textList = new List<string>() { "a", "bx", "c", "d", "e", "cj", "f", "c" };

            //Primer elemento
            var first = textList.First();

            //Primer elemento que contenga la C
            var cText = textList.First(text => text.Equals("c"));

            //Primer elemento que contenga la j
            var jText = textList.First(text => text.Contains("j"));

            //Primer elemento que contenga la Z o por defecto
            var defaultElement = textList.FirstOrDefault(text => text.Contains("z"));

            //Ultimo elemento que contenga la Z o por defecto
            var lastOrDefault = textList.LastOrDefault(text => text.Contains("z"));

            //valor unico
            var uniqueValue = textList.Single();
            var uniqueOrDefault = textList.SingleOrDefault();

            //
            int[] pares = { 0, 2, 4, 6, 8 };
            int[] otrosPares = { 0, 2, 6 };

            var misPres = pares.Except(otrosPares); //{4, 8}

        }

        //Multiples selecciones

        public static void MultiSelect()
        {

            string[] misOpiniones =
            {
                "Opinion 1, Texto 1",
                "Opinion 2, Texto 2",
                "Opinion 3, Texto 3",
            };

            var selectOpiniones = misOpiniones.SelectMany(op => op.Split(","));


            //creamos empresas

            var empresas = new[]
            {
                new Enterprise()
                {
                    Id = 1,
                    Name = "CHINALCO",
                    Employes = new[]
                    {
                        new Employe
                        {
                            Id = 1,
                            Name = "Pepe",
                            Salary = 3000,
                            Email = "jhonvd08@gmail.com"
                        },
                        new Employe
                        {
                            Id = 2,
                            Name = "Juan",
                            Salary = 4000,
                            Email = "juan@gmail.com"
                        },
                        new Employe
                        {
                            Id = 3,
                            Name = "Pedro",
                            Salary = 5000,
                            Email = "pedro@gmail.com"
                        }
                    }
                },
                new Enterprise()
                {
                    Id = 2,
                    Name = "YANACOCHA",
                    Employes = new[]
                    {
                        new Employe
                        {
                            Id = 4,
                            Name = "Maria",
                            Salary = 7000,
                            Email = "Maria@gmail.com"
                        },
                        new Employe
                        {
                            Id = 5,
                            Name = "Juana",
                            Salary = 1000,
                            Email = "juana@gmail.com"
                        },
                        new Employe
                        {
                            Id = 3,
                            Name = "Sara",
                            Salary = 5500,
                            Email = "Sara@gmail.com"
                        }
                    }
                }
            };



            //Todos los empleados
            var empleados = empresas.SelectMany(empleado => empleado.Employes);

            //saber si una lista esta vacia
            bool listaVacia = empresas.Any();

            //si tiene empleados
            bool hayEmpleados = empresas.Any(emp => emp.Employes.Any());

            //empresas que tengan empleados de mas de 1000
            bool empleadoSalarioMayor = empresas.Any( empresa => empresa.Employes
                                                    .Any(empleado => empleado.Salary >= 1000));

        }

        //
        public static void LinqCollections()
        {

            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            //Inner Join Clasico
            var elementoComun= from firstElem in firstList
                               join secondElem in secondList
                               on firstElem equals secondElem
                               select new { firstElem, secondElem};


            //Inner Join
            var comunElemento= firstList.Join(
                                secondList,
                                elemento => elemento,
                                secondElement=> secondElement,
                                (elemento,secondElement)=> 
                                new { elemento, secondElement });


            //Outer Join left
            var leftJoin = from elemento in firstList
                           join segundoElemento in secondList
                           on elemento equals segundoElemento
                           into listaTemporal
                           from temp in listaTemporal.DefaultIfEmpty()
                           where elemento != temp
                           select new { Elemneto = elemento };


            //Outer Join right
            var rightJoin = from segElemento in secondList
                            join elemento in firstList
                            on segElemento equals elemento
                            into listaTem
                            from temp in listaTem.DefaultIfEmpty()
                            where segElemento != temp
                            select new { segundo = segElemento };

        }

        //Saltar elementos

        public static void SaltarElementos()
        {
            //Skip
            var myList= new[] {1, 2,3,4,5,6,7,8,9,10};

            var primer=myList.Skip(2);// {3,4,5,6,7,8,9,10};

            var secod = myList.SkipLast(2); // {1, 2,3,4,5,6,7,8};

            var opcion = myList.SkipWhile(num => num < 4);// {4,5,6,7,8};

            //TAKE
            var prim=myList.Take(2);//{1, 2}

            var sec=myList.TakeLast(2);//{9,10}

            var tirds=myList.TakeWhile(num => num < 4);//{1, 2,3}
        }
        
        
        //Paginacion con SKIP y TAKE
        public static IEnumerable<T> GetPage<T>(IEnumerable<T> collection,int numPagina, int resultadosPagina)
        {
            int startIndex= (numPagina - 1) * resultadosPagina;
            return collection.Skip(startIndex).Take(resultadosPagina);
        }

        //Generación de variables
        static public void LinqVariables()
        {
            int[] numeros = { 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10 };
            //seleccionar los numeros cuadrDOS POR ENCIMA DE KA MEDIA
            var sobrePromedio = from numero in numeros
                            let media = numeros.Average()
                            let cuadrados = Math.Pow(numero, 2)
                            where cuadrados > media
                            select numero;

            foreach (int num in sobrePromedio)
            {
                Console.WriteLine(num);
            }
        }

        //ZIP, se van intercalando los valores
        static public void ZipLinq()
        {
            int[] numero= { 1, 2,3, 4, 5 };
            string[] stringNumeros = { "uno", "dos", "tres", "cuatro", "cinco" };

            IEnumerable<string> zipNumeros= numero.Zip(stringNumeros, (number, word) => numero+" = "+word);
            //{"1=uno","2=dos"}
        }

        //Repeat y Rango metosoa Enumerable, secuencias
        static public void RepeatRangeLinq()
        {
            //Generar 1000 primeros con Range
            IEnumerable<int> priemros1000=Enumerable.Range(0, 1000);

            //Repetir un valor n veces

            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5);//xxxxx

        }

        //
        static public void StudentsLinq()
        {
            var classRoom = new[]
            {
                new Student
                {
                    Id =1,
                    Name="Martin",
                    NotaMedia=90,
                    certificacion=true
                },
                new Student
                {
                    Id =2,
                    Name="Maria",
                    NotaMedia=95,
                    certificacion=true
                },
                new Student
                {
                    Id =3,
                    Name="lucho",
                    NotaMedia=80,
                    certificacion=true
                },
                new Student
                {
                    Id =4,
                    Name="luna",
                    NotaMedia=20,
                    certificacion=false
                },
                new Student
                {
                    Id =5,
                    Name="pepe",
                    NotaMedia=50,
                    certificacion=false
                }
            };

            //Alumnos certificados
            var certificados= from alumno in classRoom
                              where alumno.certificacion
                              select alumno;

            //Alumnos no certificados
            var noCertificacion = from alumno in classRoom
                                  where alumno.certificacion == false
                                  select alumno;

            //Alumnos nota aprobado
            var aprobados = from alumno in classRoom
                            where alumno.NotaMedia >= 50 && alumno.certificacion == true
                            select alumno;

            //Alumnos desaprobados
            var desaprobados= from alumno in classRoom
                              where alumno.NotaMedia < 50 && alumno.certificacion==false
                              select alumno.Name;

            //GoupBy

            var certifiedGrupBy = classRoom.GroupBy( student => student.certificacion);

            //dos grupos {no certificados} {los certificados}

            foreach (var grupo in certifiedGrupBy)
            {
                foreach (var student in grupo)
                {
                    Console.WriteLine(student.Name);
                }
            }
        }

        //all tdos los valores ANY cualquiera de los valores

        static public void AllLinq()
        {
            var numb = new List<int>() { 1, 2, 3, 4, 5 };

            bool todosMenoresqu10 = numb.All(x => x < 10); //true

            bool todosMayoresque2 = numb.All(x => x > 2); //false


            var listaVacia = new List<int>();

            bool todosNumerosMayoresCero= numb.All(x => x >= 0);//true
        
        }

        static public void RelationsLinq()
        {
            List<Post>  posts = new List<Post>()
            {
                new Post()
                {
                    Id= 1,
                    Title="Mi primer post",
                    Contenido="Mi ptimer contenido",
                    Comentario= new List<Comentario>()
                    {
                        new Comentario()
                        {
                            Id= 1,
                            Created= DateTime.Now,
                            Title="Mi primer comentario",
                            Contenido="Priemr contenido del primer comentario"
                        },
                        new Comentario()
                        {
                            Id= 2,
                            Created= DateTime.Now,
                            Title="Mi segundo comentario",
                            Contenido="Segundo contenido del segundo comentario"
                        }
                    }
                },
                new Post()
                {
                    Id= 2,
                    Title="Mi segundo post",
                    Contenido="Mi segundo contenido",
                    Comentario= new List<Comentario>()
                    {
                        new Comentario()
                        {
                            Id= 3,
                            Created= DateTime.Now,
                            Title="Mi tercer comentario",
                            Contenido="tercer contenido del tercer comentario"
                        },
                        new Comentario()
                        {
                            Id= 4,
                            Created= DateTime.Now,
                            Title="Mi cuarto comentario",
                            Contenido="cuarto contenido del cuarto comentario"
                        }
                    }
                }
            };

            var comentarioConContenido = posts.SelectMany(post => post.Comentario, 
                                                                (post, coment) => new {PostId=post.Id, ComentId=coment.Contenido});
        }



    }
}
