
//permirtir al usuario realizar una busqueda dentro de una lista string,en caso de no haber coincidencias
//permitir al usuario ingresar un nuevo elemento, en caso de encontrar coincidencias,
//mostrar el resultado al usuario

using PetFamily;
using PetFamily.Models;

PetsClass objPetClas = new PetsClass();
List<int> ids = new List<int>();
int answerUser;

bool answerBool = false;

bool resp = true;

do

{



    Console.WriteLine("Elije la opción que con la que desees interactuar:");

    Console.WriteLine("1.- Insertar");

    Console.WriteLine("2.- Consultar ");

    Console.WriteLine("3.- Actualizar ");

    Console.WriteLine("4.- Eliminar ");

    string optionUser = Console.ReadLine();

    int.TryParse(optionUser, out answerUser);

    switch (answerUser)
    {
        case 1:
            var datosUsuario = PedirDatosPet();
            objPetClas.InsertPets(datosUsuario.Name, datosUsuario.Description, datosUsuario.Gender, datosUsuario.IsStillAlive);

            break;
        case 2:
            ConsultMethodPets();
            break;
        case 3:
            List<MyPets> petlst = ConsultMethodPets();
            int registroActualizar = CorrectInputForInt("Cual deseas actualizar", petlst.Select(v => v.Id).ToList());
            var datos = PedirDatosPet();
            objPetClas.UpdatePets(registroActualizar, datos.Name, datos.Description, datos.Gender[0], datos.IsStillAlive);
            break;
        case 4:

            List<MyPets> ConsultPetList = ConsultMethodPets();
            int registroEliminar = CorrectInputForInt("elige el numero a eliminar", ConsultPetList.Select(v => v.Id).ToList());

            bool dltpetlst = objPetClas.DeletePets(registroEliminar);
            if (dltpetlst)
            {
                Console.WriteLine("Mascota Elimiada");
            }
            break;
            default:
            //Console.WriteLine("Opcion invalida");
           
            Console.WriteLine("Cuantos años tienes?");
            bool resultParseo = int.TryParse(Console.ReadLine(), out int edad);
            if (resultParseo)
            {
                string respuesta = UserAge(edad);
                Console.WriteLine(respuesta);
            }
            break;
    }

    Console.WriteLine($"Desea intentarlo  S/N");
    resp = Console.ReadLine().ToLower().Contains("s");

} while (resp);



MyPets PedirDatosPet()
{
    Console.WriteLine("Ingresa el nombre de tu mascota");
    string nombre = Console.ReadLine();
    Console.WriteLine("Ingresa su descripcion");
    string descripcion = Console.ReadLine();
    Console.WriteLine("Ingresa su genero M / H");
    string genero = Console.ReadLine();
    Console.WriteLine("sigue con vida?");
    bool vive = Console.ReadLine().Contains("si");
    var response = new MyPets
    {
        Name = nombre,
        Description = descripcion,
        Gender = genero,
        IsStillAlive = vive
    };
    return response;
}


int CorrectInputForInt(string description, List<int> limit)
{
    bool response = false;
    int finalValue = 0;
    while (!response)
    {
        Console.WriteLine(description);
        var value = Console.ReadLine();
        response = int.TryParse(value, out finalValue);
        var exist = limit.FirstOrDefault(value => value == finalValue);
        if (finalValue == 0 || exist == 0)
        {
            response = false;
            Console.WriteLine("incorrect data, try again.");
        }
    }
    return finalValue;
}

List<MyPets> ConsultMethodPets()
{
    List<MyPets> SelectPetList = objPetClas.GetNamePets();
    foreach (var pet in SelectPetList)
    {
        Console.WriteLine($"{pet.Id} | {pet.Name} | {pet.Description} | {pet.Gender} | {(pet.IsStillAlive ? "Vivo" : "sin vida")} ");
    }

    return SelectPetList;
}

string UserAge(int edad)
{
    string resp = null;
 
    if (edad > 18)
    {

        resp = "mayor";
    }
    else
    {
        resp = "menor";
    }

    return resp;
}