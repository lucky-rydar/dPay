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

QString ApiWrapper::add_card(QString token, QString name, QString currency)
{
    return makeRequest(baseUrl + "card/add/" + token + "/" + name + "/" + currency);
}

QString ApiWrapper::remove_card(QString token, QString card_token)
{
    return makeRequest(baseUrl + "card/remove/" + token + "/" + card_token);
}

QString ApiWrapper::cards(QString token)
{
    return makeRequest(baseUrl + "card/cards/" + token);
}

QString ApiWrapper::rename_card(QString token, QString card_token, QString new_name)
{
    return makeRequest(baseUrl + "card/rename/" + token + "/" + card_token + "/" + new_name);
}

QString ApiWrapper::set_default_card(QString token, QString card_token)
{
    return makeRequest(baseUrl + "card/set_default/" + token + "/" + card_token);
}

QString ApiWrapper::get_card_data(QString token, QString card_id)
{
    return makeRequest(baseUrl + "card/get_card_data/" + token + "/" + card_id);
}

QString ApiWrapper::send_by_card(QString token, QString from_card, QString to_card, QString amount) // later use description also
{
    return makeRequest(baseUrl + "transaction/send_by_card/" + token + "/" + from_card + "/" + to_card + "/" + amount);
}

QString ApiWrapper::send_by_username(QString token, QString from_username, QString to_username, QString amount)
{
    return makeRequest(baseUrl + "transactions/send_by_username/" + token + "/" + from_username + "/" + to_username + "/" + amount);
}

QString ApiWrapper::transactions(QString token)
{
    return makeRequest(baseUrl + "transaction/transactions/" + token);
}

QString ApiWrapper::create_donation(QString token, QString receiver_card_token, QString title, QString description)
{
    return makeRequest(baseUrl + "donation/create_donation/" + token + "/" + receiver_card_token + "/" + title + "/" + description);
}

QString ApiWrapper::donations(QString token)
{
    return makeRequest(baseUrl + "donation/donations/" + token);
}

QString ApiWrapper::donation_by_token(QString donation_token)
{
    return makeRequest(baseUrl + "donation/donation_by_token/" + donation_token);
}

QString ApiWrapper::donate(QString token, QString from_card, QString donation_token, QString amount)
{
    return makeRequest(baseUrl + "donation/donate/" + token + "/" + from_card + "/" + donation_token + "/" + amount);
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
