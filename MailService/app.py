from flask import Flask, render_template, request, url_for, jsonify
from waitress import serve
from flask_mail import Mail, Message
from flask_swagger_ui import get_swaggerui_blueprint
from datetime import date

import uuid
import os
import re

app = Flask(__name__)

### swagger specific ###
SWAGGER_URL = '/swagger'
API_URL = '/static/swagger.json'
SWAGGERUI_BLUEPRINT = get_swaggerui_blueprint(
    SWAGGER_URL,
    API_URL,
    config={
        'app_name': "Email-service-swagger"
    }
)
app.register_blueprint(SWAGGERUI_BLUEPRINT, url_prefix=SWAGGER_URL)

mail_settings = {
    "MAIL_SERVER": 'smtp.gmail.com',
    "MAIL_PORT": 465,
    "MAIL_USE_TLS": False,
    "MAIL_USE_SSL": True,
    "MAIL_USERNAME": 'skakkristiansen.test@gmail.com',
    "MAIL_PASSWORD": 'xfu63wmj'
}

app.config.update(mail_settings)
mail = Mail(app)


@app.after_request
def add_hostname_header(response):
    env_host = str(os.environ.get('HOSTNAME'))
    hostname = re.findall('[a-z]{3}-\d$', env_host)
    if hostname:
            response.headers["SP-LOCATION"] = hostname
    return response


@app.route('/')
def index():
	msg = Message(subject="Hello", 
					sender=app.config.get("MAIL_USERNAME"),
					recipients=["skakkristiansen.test@gmail.com"],
					body="Test from python, this is body")
	mail.send(msg)

@app.route('/newcustomer', methods=['POST'])
def newcustomer():
    req_data = request.json
    email = req_data['email']
    msg = Message(subject='''Welcome to CompuSuite''', 
					sender=app.config.get("MAIL_USERNAME"),
					recipients=["skakkristiansen.test@gmail.com"])
    msg.html = '''</head>
<body>
  <table class="email-wrapper" width="100%" cellpadding="0" cellspacing="0">
    <tr>
      <td align="center">
        <table class="email-content" width="100%" cellpadding="0" cellspacing="0">
          <!-- Logo -->
          <tr>
            <td class="email-masthead">
              <a class="email-masthead_name">CompuSuite</a>
            </td>
          </tr>
          <!-- Email Body -->
          <tr>
            <td class="email-body" width="100%">
              <table class="email-body_inner" align="center" width="570" cellpadding="0" cellspacing="0">
                <!-- Body content -->
                <tr>
                  <td class="content-cell">
                    <h1>Velkommen</h1>
                    <p>Vi i CompuSoft glæder os over at du har valgt vores platform til at booke din næste rejse </p>
                    <br/>
                      </tr>
                      <tr>
                          <td align="left">
                              <br/>
                              <p>Du kan læse mere om platformen på vores hjemmeside <a href="#">her</a></p>
                              
                              
                          </td>
                      </tr>
                    </table>
                    <p>Venlig Hilsen,<br>CompuSoft</p>
                    <!-- Sub copy -->
                    <table class="body-sub">
                      <tr>
                        <td>
                          <p class="sub">Hvis du har problemer med at klikke på knappen kan du istedet kopierer følgende link i browseren
                          </p>
                          <p class="sub"><a href="{{action_url}}">35.233.85.187</a></p>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td>
              <table class="email-footer" align="center" width="570" cellpadding="0" cellspacing="0">
                <tr>
                  <td class="content-cell">
                    <p class="sub center">
                      CompuSoft
                    </p>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html> '''
    mail.send(msg)
    return 'success'  

@app.route('/newbooking', methods=['POST']) #GET requests will be blocked
def newbooking():
    req_data = request.json
    arrival = req_data['arrival']
    email = req_data['email']
    depature = req_data['depature']
    customer_name = req_data['price']
    item_des = req_data['itemDescription']
    currency = req_data['currency']
    price = req_data['price']
    item_name = req_data['itemName']
    item_days = req_data['bookedDays']
    today = date.today()
    msg = Message(subject='''Booking confirmation for item: {}'''.format(item_name), 
					sender=app.config.get("MAIL_USERNAME"),
					recipients=["skakkristiansen.test@gmail.com"])
    msg.html = '''
    <h1>Confirmation of booking</h1>
<p>The following booking has been registered. Please review.</p>
<div class="tg-wrap">
<table class="tg" style="undefined;table-layout: fixed; width: 563px;"><colgroup> <col style="width: 220px;" /> <col style="width: 220px;" /> <col style="width: 220px;" /> <col style="width: 220px;" /> </colgroup>
<thead>
<tr>
<th class="tg-z6g2">Booking of Item:</th>
<th class="tg-z6g2">{}</th>
<th class="tg-z6g2">Date:</th>
<th class="tg-z6g2">{}</th>
</tr>
</thead>
<tbody>
<tr>
<td class="tg-8rb3" colspan="2">Price:&nbsp;{}&nbsp;{}</td>
<td class="tg-8rb3">&nbsp;</td>
<td class="tg-8rb3">&nbsp;</td>
</tr>
<tr>
<td class="tg-z6g2" colspan="2">Arrival:&nbsp;{}</td>
<td class="tg-z6g2" colspan="2">Departure:&nbsp;{}</td>
</tr>
<tr>
<td class="tg-8rb3">Description:</td>
<td class="tg-8rb3" colspan="3">{}</td>
</tr>
<tr>
<td class="tg-0lax">&nbsp;</td>
<td class="tg-0lax">&nbsp;</td>
<td class="tg-0lax">&nbsp;</td>
<td class="tg-0lax">&nbsp;</td>
</tr>
<tr>
<td class="tg-8rb3">&nbsp;</td>
<td class="tg-8rb3">&nbsp;</td>
<td class="tg-8rb3">&nbsp;</td>
<td class="tg-8rb3" style="text-align: right;">{}</td>
</tr>
<tr>
<td class="tg-fia5">&nbsp;</td>
<td class="tg-fia5">&nbsp;</td>
<td class="tg-fia5">&nbsp;</td>
<td class="tg-fia5" style="text-align: right;">{}</td>
</tr>
</tbody>
</table>
</div>
    '''.format(item_name, today, price, currency, arrival, depature, item_des, customer_name, email)
    mail.send(msg)
    return 'success' 

if __name__ == "__main__":
    serve(app, listen='*:80')