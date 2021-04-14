#include "apiwrapper.h"

ApiWrapper::ApiWrapper(QObject *parent) : QObject(parent)
{
    baseUrl = "http://localhost:5000/api/"; // change on release
}

RegisterStatus ApiWrapper::registration(QString username, QString email, QString phone, QString password)
{
    auto jsonReply = makeRequest(baseUrl+"user/register/" + username + "/" + email + "/" + phone + "/" + password);

    if(jsonReply.isEmpty())
    {
        return RegisterStatus{ false };
    }
    else
    {
        return RegisterStatus{ jsonReply["registered"].toBool() };
    }
}

LoginStatus ApiWrapper::login(QString username, QString password)
{
    auto jsonReply = makeRequest(baseUrl + "user/login/" + username + "/" + password);

    return LoginStatus{ jsonReply["logined"].toBool(),
                        jsonReply["token"].toString(),
                        jsonReply["email"].toString(),
                        jsonReply["phone"].toString(),
                jsonReply["username"].toString() };
}

QJsonDocument ApiWrapper::makeRequest(QString url)
{
    QNetworkAccessManager accessManager;

    QEventLoop loop;
    QObject::connect(&accessManager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);

    auto reply = accessManager.get(QNetworkRequest(QUrl(url)));

    loop.exec();

    auto resJson = QJsonDocument::fromJson(reply->readAll());
    qDebug() << resJson;

    return resJson;
}
