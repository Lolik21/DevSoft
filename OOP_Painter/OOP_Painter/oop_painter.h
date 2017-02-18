#ifndef OOP_PAINTER_H
#define OOP_PAINTER_H

#include <QMainWindow>
#include <QGraphicsView>
#include <QGraphicsScene>


#include <myellipse.h>
#include <myline.h>
#include <myrectangle.h>
#include <qpaintpattr.h>

namespace Ui {
class OOP_painter;
}

class OOP_painter : public QMainWindow
{
    Q_OBJECT

public:
    explicit OOP_painter(QWidget *parent = 0);
    ~OOP_painter();

private:
    Ui::OOP_painter *ui;
    QPaintPattr *Painter;

};

#endif // OOP_PAINTER_H
