#ifndef APIWRAPPER_H
#define APIWRAPPER_H

#include <QObject>
#include <QPointer>
#include <QtNetwork>
#include <QJsonObject>

class ApiWrapper : public QObject
{
    Q_OBJECT
public:
    explicit ApiWrapper(QObject *parent = nullptr);

    // user
    Q_INVOKABLE QString registration(QString username, QString email, QString phone, QString password);
    Q_INVOKABLE QString login(QString username, QString password);
    Q_INVOKABLE QString change_phone(QString token, QString new_phone);

    // card
    Q_INVOKABLE QString add_card(QString token, QString name, QString currency);
    Q_INVOKABLE QString remove_card(QString token, QString card_token);
    Q_INVOKABLE QString cards(QString token);
    Q_INVOKABLE QString rename_card(QString token, QString card_token, QString new_name);
    Q_INVOKABLE QString set_default_card(QString token, QString card_token);
    Q_INVOKABLE QString get_card_data(QString token, QString card_token);

    // transaction
    Q_INVOKABLE QString send_by_card(QString token, QString from_card, QString to_card, QString amount);
    Q_INVOKABLE QString send_by_username(QString token, QString from_username, QString to_username, QString amount);
    Q_INVOKABLE QString transactions(QString token);

    //donation
    Q_INVOKABLE QString create_donation(QString token, QString receiver_card_token, QString title, QString description);
    Q_INVOKABLE QString donations(QString token);
    Q_INVOKABLE QString donation_by_token(QString donation_token);
    Q_INVOKABLE QString donate(QString token, QString from_card, QString donation_token, QString amount);

private:
    QString makeRequest(QString url);

private:
    QString baseUrl;

signals:

};

#endif // APIWRAPPER_H
