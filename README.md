# Wordle Por Olav Martos
## **Fase 1**
La fase uno del Proyecto de Wordle consistia en crear una version del popular juego Wordle que funcionase en terminal.

En la epoca en la que hicimos esta fase, todavia no sabiamos parametrizar, por lo que todo se encontraba metido en la funcion Main()

Para más informacion vea el documento de google: [Proyecto Trocal: Wordle Fase 1](https://docs.google.com/document/d/1AloybXTnDLaIVGlVvzH9BypDNyhS0cdzTFMylKEqmOY/edit?usp=sharing)

## **Fase 2**

### Parametrizacion Nivel 1
Lo primero que se hizo al entrar en esta fase fue parametrizar el codigo donde salieron 5 funciones void.

### Parametrizacion Nivel 2
La siguiente parte fue hacer que el usuario pueda jugar repetidamente hasta que el quiera, por lo que se creo la funcion WannaPlay().

Tambien en esta segunda parte se creo una funcion que no era void. Y esa es UserInteraction.

Otra parte que se hizo fueron las funciones de pintar de distintos colores dependiendo de las condiciones del juego.

Tambien se creo una funcion de victoria que volvia a llamar a WannaPlay()

## **Fase 3**
La Fase 3 del Wordle, consistia en usar ficheros y test unitarios.

Para ello se crearon tres ficheros, un fichero con frases genericas que no tienen que ver con el juego en si, y los otros dos que contienen frases para mostrar durante la partida que son los de idioma.

Por lo que se añadieron diversas lineas relacionadas con la lectura o la escritura de ficheros a lo largo del codigo

### Config.txt
Este fichero es el que contiene informacion generica, se usa para mostrar el contenido del menu o a que pertenecen cada valor a la hora de mostrar el historico

### Idioma.txt
Frases que se muestran durante la propia partida.

### Menu
En el apartado de Config.txt hemos hablado de un menu, este menu tenia que mostrar una forma para cambiar el idioma. En este codigo te permite tambien jugar.
Otra funcion es mostrar un Historico con la partida guardada.

### __Historico__
Para poder mostrar el historico, necesitabamos pregunta el nombre al usuario y pasar ese nombre a traves de todas las funciones necesarias hasta llegar a la funcion **Save()** quien se encarga de guardar las partidas de una forma concreta siguiendo esta estructura:

>nombre;palabraAleatoria;idioma;estado

Esta estructura fue al principio. Cuando solo se estaba buscando lo más basico.

En este punto del codigo, el fichero de idioma tenia en una linea las 20 palabras, debido a que para manipular en un futuro ese documento seria complicado, se sustituyo esa linea por una ruta que el codigo leia para obtener las palabras de ese idioma

#### **__Mejoras en el historico__**
Se modifico la estructura del archivo de guardado por la siguiente.

>nombre;palabraAleatoria;idioma;estado;numeroEstado

La estructura era la misma pero el numeroEstado varia entre 0 y 1 para dictaminar de que color pintar la palabra del estado

### **100 Palabras**
Llegados a este punto en el que el codigo funciona perfectamente llego la hora de ir a los archivos donde estaban guardadas las palabras y pasar de esas 20 palabras a 100.

### **Test Unitarios**
Una vez el codigo se encontraba en este estado, llego la hora de hacer los test para saber que funcionan correctamente. Uno de los test devolvia la palabra aleatoria por lo que se tuve que crear una nueva funcion y mover lineas a otra funcion con tal de que funcionase correctacmente.

### **Ideas de ultima hora**
Una idea que se tuvo de ultima hora fue guardar el numero de intentos, por suerte se podia usar la iteracion de la funcion Play() y se añadio una especie de Leyenda en la cabezera del Historico para entenderlo mejor.

Otra idea que se tuvo a ultima hora fue guardar en una lista todas las palabras escritas por el usuario en el fichero de partida y mostrarlo en el historico sin colores.

Esto se logro modificando la estructura del fichero de guardado por la siguiente:
>usuario;palabraAleatoria;idioma;EstadoPartida;NumeroEstado;Intentos\
>palabra1;palabra2;palabra3;palabra4;palabra5;palabra6

### Finalizacion de la Fase 3
Una vez ya no quedaban más ideas por añadir al Wordle, llego la hora de hacer el merge de la rama de la Fase 3 y que solo quede la rama Master.