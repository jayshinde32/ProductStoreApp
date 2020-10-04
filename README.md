# ProductStoreApp

1. Download all code on your machine.
2. open with visual studio 2017/2019(better is 2019)
3. Change the connection string in web.config file in Web Api Application.
4. Set the log generate file path(Key name is 'LogPath' i set it E:\) in web.config file in Web Api Application.
4. Set the log generate file path(Key name is 'LogPath' i set it E:\) in web.config file in Product Store App.
5. Run the both application parallelley(Web Api & Product Store App( You can set it in the soluntion proprties Multiple startup project option)).
6. Once Web Api Application up and run DB Will generate automatically(here i used code first approch in Entity FW).
7. Check the Web Api localhost url address after run the Web Api and compaire that address in Web.config file(Key name is 'ApiUrl') in Product Store App.
   If your web Aip url is same with mentioned url then leave it. if url is different then copy past your Api url there.
8. After run the both applcaition successfully then create category and unit first in Product store App and then create product.



 
