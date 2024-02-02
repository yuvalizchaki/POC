import express, { Request, Response } from "express";
import {
  getAllOrders,
  findOrderById,
  addOrder,
  updateOrder,
  deleteOrder,
} from "../services/orders.service";
import { sendToWebHook } from "../services/webhook.service";

const router = express.Router();

router.get(
  "/orders",
  async (req: Request<{}, {}, {}, { count?: string }>, res: Response) => {
    const count = parseInt(req.query.count || "0", 10);
    const orders = getAllOrders(!isNaN(count) ? count : undefined);
    res.json(orders);
  }
);

router.post("/orders", async (req, res) => {
  const newOrder = addOrder();
  sendToWebHook('/orders', 'post', newOrder);
  res.status(201).json(newOrder);
});

router.get("/orders/:id", async (req, res) => {
  const order = findOrderById(req.params.id);
  if (!order) {
    return res.status(404).send({error: "Order not found"});
  }
  res.json(order);
});

router.put("/orders/:id", async (req, res) => {
  const updatedOrder = updateOrder(req.params.id, req.body);
  if (!updatedOrder) {
    return res.status(404).send({error: "Order not found"});
  }
  sendToWebHook(`/orders/${req.params.id}`, 'put', updatedOrder);
  res.json(updatedOrder);
});

router.delete("/orders/:id", async (req, res) => {
  const success = deleteOrder(req.params.id);
  if (!success) {
    return res.status(404).send({error: "Order not found"});
  }
  sendToWebHook(`/orders/${req.params.id}`, 'delete');
  res.status(204).send();
});

export default router;
