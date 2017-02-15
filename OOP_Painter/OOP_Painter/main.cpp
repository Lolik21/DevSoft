#include "oop_painter.h"
#include <QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    OOP_painter w;
    w.show();

    return a.exec();
}
