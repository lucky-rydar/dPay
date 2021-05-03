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

QString ApiWrapper::change_phone(QString token, QString new_phone)
{
    return makeRequest(baseUrl + "user/change_phone/" + token + "/" + new_phone);
}

QString ApiWrapper::add(QString token, QString number, QString month_exp, QString year_exp, QString cvv)
{
    return makeRequest(baseUrl + "card/add/" + token + "/" + number + "/" + month_exp + "/" + year_exp + "/" + cvv);
}

QString ApiWrapper::remove(QString token, QString card_id)
{
    return makeRequest(baseUrl + "card/remove/" + token + "/" + card_id);
}

QString ApiWrapper::cards(QString token)
{
    return makeRequest(baseUrl + "card/cards/" + token);
}

QString ApiWrapper::rename(QString token, QString card_id, QString new_name)
{
    return makeRequest(baseUrl + "card/rename/" + token + "/" + card_id + "/" + new_name);
}

QString ApiWrapper::set_default(QString token, QString card_id)
{
    return makeRequest(baseUrl + "card/set_default/" + token + "/" + card_id);
}

QString ApiWrapper::get_card_data(QString token, QString card_id)
{
    return makeRequest(baseUrl + "card/get_card_data/" + token + "/" + card_id);
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
