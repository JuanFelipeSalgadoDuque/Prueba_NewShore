1. En la carpeta TEST están contenidos los dos archivos de prueba de la aplicación (CONTENIDO.txt y REGISTRADOS.txt) y una carpeta adicional donde se encuentra la colección de Postman para realizar el test de integración.

2. El archivo RESULTADOS.txt es el archivo que contiene la solución al problema, se almacena en el escritorio una vez finaliza el flujo del programa.

3. El archivo de LOGS está configurado para almacenarse en la ruta "C:\logs\PruebaNewShoreLog.log"

4. El item de la prueba para Crear y consumir un servicio web WCF (REST / SOAP) no fue resuelto ya que por desconocimiento del tema.

5. Los métodos en la clase FileManagement que están declarados con el modificador de acceso internal, en un principio estaban como private pero se han cambiados con el proposito de que fueran accesibles desde los test unitarios.