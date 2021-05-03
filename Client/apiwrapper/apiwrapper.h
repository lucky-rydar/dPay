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
    Q_INVOKABLE QString add(QString token, QString number, QString month_exp, QString year_exp, QString cvv);
    Q_INVOKABLE QString remove(QString token, QString card_id);
    Q_INVOKABLE QString cards(QString token);
    Q_INVOKABLE QString rename(QString token, QString card_id, QString new_name);
    Q_INVOKABLE QString set_default(QString token, QString card_id);
    Q_INVOKABLE QString get_card_data(QString token, QString card_id);

    // transaction

private:
    QString makeRequest(QString url);

private:
    QString baseUrl;

signals:

};

#endif // APIWRAPPER_H
