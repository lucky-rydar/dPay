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

    // card

    // transaction

private:
    QString makeRequest(QString url);

private:
    QString baseUrl;

signals:

};

#endif // APIWRAPPER_H
