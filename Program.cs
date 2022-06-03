
//permirtir al usuario realizar una busqueda dentro de una lista string,en caso de no haber coincidencias
//permitir al usuario ingresar un nuevo elemento, en caso de encontrar coincidencias,
//mostrar el resultado al usuario

using PetFamily;

PetsClass petDbObj = new PetsClass();

////bool comp = petDbObj.InsertPets("Los perritos", "perrillos que acaban de nacer","M",true);
////Console.WriteLine(comp ? "se pudo":"no se pudo");
bool resp = false;
List<string> NameList = petDbObj.GetNamePets();
List<string> coincidencias = new List<string>();    
while (!resp)
{
    Console.WriteLine("ingresa tu busqueda:");
    string nameDog = Console.ReadLine();


    foreach (var name in NameList)
    {
        if (name.Contains(nameDog)) //determina si existe una coincidencia entre strings y regresa un boolean
        {
            coincidencias.Add(name);
            resp = true;
        }
    }

    if (!resp)
    {
        Console.WriteLine("No existen coicidencias");
        Console.WriteLine("Ahora Ingresa el nombre:");
        string newDog = Console.ReadLine();
        NameList.Add(newDog);
        Console.WriteLine($"el nombre {newDog} ha sido agregado");
    }
    else
    {
        foreach (var perros in coincidencias)
        {
            Console.WriteLine(perros);
        }
    }



}
