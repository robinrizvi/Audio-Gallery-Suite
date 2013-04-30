ABOUT
-------------
**Audio-Gallery-Suite** is a complete audio gallery solution made with HTML5/CSS3/Jquery-JS/PHP-ajax/C# that includes a web audio gallery and a software (both web and windows based) for managing the web audio gallery.

DEMOS
-------------
Watch video demonstration on youtube <http://youtu.be/IEB4hLtcTiU>  
<http://projects.robinrizvi.info/audio-gallery-suite/>  
<http://projects.robinrizvi.info/audio-gallery-suite/management/audiogallery/login.html>

Gallery Management  
-------------  
#### Windows based software (Deprecated) : ####
The windows based software that was earlier used to manage the audio gallery suite (CRUD management for playlists and audios) has not been tested with the latest commits made and may or may not fucntion correctly.

#### Web based application (Recommended) : ####
A web based management panel (named as Gallardmin) has been added. **Gallardmin** is a web based gallery management module that helps in management of the audio gallery suite using a web based interface. It uses the latest web technologies and a cleaner intuitive interface.

USAGE (includes usage instructions for setup of Audio-Gallery-Suite using Gallardmin-web based management interface only)
-------------
1. Download this zip:
<https://github.com/robinrizvi/Audio-Gallery-Suite/archive/master.zip>

2. Execute these sql scripts (present in Resources/SQL Script) in your mysql server:
    1. **audiogallery_db.sql**
    2. **create_user_[optional].sql**  
                                              
     
3. Configure **Gallery/php/config.php** if the there are any changes to the database or the database user other than the default ones assumed.

4. **Copy/upload the contents of Gallery folder** from the zip to your server where-ever you want

5. Go to **http://root_url_to_gallery_folder/management/audiogallery/login.html**

6. Type **superuser** and **superuser** as username and password respectively

7. Add playlist and add audios under that playlist

8. Go to your **http://root_url_to_gallery_folder/index.html to view your gallery**

CODE DESCRIPTION
-------------
<http://www.codeproject.com/Articles/333670/Audio-Gallery-Suite-A-complete-audio-gallery-solut>  
<http://blog.robinrizvi.info/?p=55>
 
AUTHOR
-------------
**Robin Rizvi**  
**Email**: <mail@robinrizvi.info>  
**Website**: <http://robinrizvi.info/>  
**Blog**: <http://blog.robinrizvi.info>

CREDITS
-------------
View the Credits.txt file

LICENSE
-------------
Licensed under the MIT license.  
<http://www.opensource.org/licenses/MIT>
