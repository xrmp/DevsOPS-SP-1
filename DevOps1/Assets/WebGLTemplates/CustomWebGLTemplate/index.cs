< !DOCTYPE html >
< html lang = "ru" >
< head >
    < meta charset = "utf-8" >
    < meta name = "viewport" content = "width=device-width, initial-scale=1.0" >
    < title > Custom WebGL Template</title>
    <style>
        body {
            margin: 0;
padding: 0;
background: linear - gradient(135deg, #667eea 0%, #764ba2 100%);
            font - family: 'Arial', sans - serif;
        }
        
        #custom-header {
            text - align: center;
padding: 20px;
background: rgba(255, 255, 255, 0.9);
margin - bottom: 20px;
box - shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        
        #custom-header h1 {
            margin: 0;
color: #333;
            font - size: 24px;
        }
        
        #unity-container {
            width: 960px;
margin: 0 auto;
border - radius: 10px;
overflow: hidden;
box - shadow: 0 10px 30px rgba(0,0,0,0.2);
        }
        
        #custom-footer {
            text - align: center;
padding: 20px;
color: white;
margin - top: 20px;
        }
        
        .custom - watermark {
position: fixed;
    bottom: 10px;
right: 10px;
color: rgba(255, 255, 255, 0.5);
    font - size: 12px;
}
    </ style >
</ head >
< body >
    < !--Кастомный заголовок-- >
    < div id = "custom-header" >
        < h1 >🎮 Custom WebGL Template - DevOps Задание 2</h1>
        <p>Кастомный шаблон успешно подключен!</p>
    </div>
    
    <!-- Контейнер для Unity -->
    <div id="unity-container">
        <div id="unity-loader"></div>
        <div id="unity-progress"></div>
    </div>
    
    <!-- Кастомный футер -->
    <div id="custom-footer">
        <p>Собрано с помощью кастомного WebGL Template</p>
    </div>
    
    <!-- Водяной знак -->
    <div class= "custom-watermark" >
        CustomWebGLTemplate v1.0
    </div>
    
    <!-- Unity WebGL загрузчик -->
    <script src="Build/UnityLoader.js"></script>
    <script>
        var unityInstance = UnityLoader.instantiate("unity-container", "Build/Build.json", {onProgress: UnityProgress});
    </ script >
</ body >
</ html >