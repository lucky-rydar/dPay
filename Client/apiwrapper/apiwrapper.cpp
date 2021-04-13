#include "apiwrapper.h"

ApiWrapper::ApiWrapper(QObject *parent) : QObject(parent)
{
    baseUrl = "http://localhost:5000/api/";
}

RegisterStatus ApiWrapper::registration(QString username, QString email, QString phone, QString password)
{
    QNetworkAccessManager accessManager;
    auto reply = accessManager.get(QNetworkRequest(QUrl(baseUrl + "user/register/" + username + "/" + email + "/" + phone + "/" + password)));

    QEventLoop loop;
    QObject::connect(&accessManager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);
    loop.exec();

    auto jsonReply = QJsonDocument::fromJson(reply->readAll());

    return RegisterStatus{ jsonReply["registered"].toBool() };
}

LoginStatus ApiWrapper::login(QString username, QString password)
{
    QNetworkAccessManager accessManager;
    auto reply = accessManager.get(QNetworkRequest(QUrl(baseUrl + "user/login/" + username + "/" + password)));

    QEventLoop loop;
    QObject::connect(&accessManager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);
    loop.exec();

    auto jsonReply = QJsonDocument::fromJson(reply->readAll());

    return LoginStatus{ jsonReply["logined"].toBool(),
                        jsonReply["token"].toString(),
                        jsonReply["email"].toString(),
                        jsonReply["phone"].toString(),
                        jsonReply["username"].toString() };
}
