#include "apiwrapper.h"

ApiWrapper::ApiWrapper(QObject *parent) : QObject(parent)
{
    baseUrl = "http://localhost:5000/api/"; // change on release
}

QString ApiWrapper::registration(QString username, QString email, QString phone, QString password)
{
    return makeRequest(baseUrl+"user/register/" + username + "/" + email + "/" + phone + "/" + password);
}

QString ApiWrapper::login(QString username, QString password)
{
    return makeRequest(baseUrl + "user/login/" + username + "/" + password);
}

QString ApiWrapper::makeRequest(QString url)
{
    QNetworkAccessManager accessManager;

    QEventLoop loop;
    QObject::connect(&accessManager, &QNetworkAccessManager::finished, &loop, &QEventLoop::quit);

    auto reply = accessManager.get(QNetworkRequest(QUrl(url)));

    loop.exec();

    return reply->readAll();
}
