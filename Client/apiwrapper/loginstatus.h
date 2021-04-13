#ifndef LOGINSTATUS_H
#define LOGINSTATUS_H

#include <QObject>

class LoginStatus
{
    Q_GADGET
public:
    bool logined;
    QString token;
    QString username;
    QString email;
    QString phone;
private:
    Q_PROPERTY(bool logined MEMBER logined)
    Q_PROPERTY(QString token MEMBER token)
    Q_PROPERTY(QString username MEMBER username)
    Q_PROPERTY(QString email MEMBER email)
    Q_PROPERTY(QString phone MEMBER phone)
};

Q_DECLARE_METATYPE(LoginStatus)

#endif // LOGINSTATUS_H
