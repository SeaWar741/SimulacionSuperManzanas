from flask import Flask,render_template
from flask import jsonify
from werkzeug.wrappers import Request, Response
import json

app = Flask(__name__)

#Ruta para mostrar la pagina principal
@app.route('/')
def run_index():
    #return index.html from Build0.3/
    return render_template('index.html')

#Ruta para mostrar la simulacion
@app.route('/simulation')
def run_simulation_game():
    #return index.html from Build0.3/
    return render_template('game.html')

config = {
    "DEBUG": True  # run app in debug mode
}

app.config.from_mapping(config)

if __name__ == '__main__':
    from livereload import Server
    server = Server(app.wsgi_app)
    server.serve(host = '0.0.0.0',port=3000)