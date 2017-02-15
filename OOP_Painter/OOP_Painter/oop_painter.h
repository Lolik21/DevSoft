#ifndef OOP_PAINTER_H
#define OOP_PAINTER_H

#include <QMainWindow>

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
};

#endif // OOP_PAINTER_H
