import express from 'express';
import ordersRouter from './routes/orders.routes';
import path from 'path';

const app = express();
const port = 8008;

app.use(express.json());
app.use(ordersRouter);

// Serve static files from the "public" directory
app.use(express.static('public'));

// Specific route for gui.html
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'public', 'gui.html'));
});

app.listen(port, () => {
    console.log(`Server running on port ${port}`);
});
