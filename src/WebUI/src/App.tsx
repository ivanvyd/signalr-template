import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import './App.css';

interface Item {
  id: number;
  name: string;
}

const ItemList: React.FC = () => {
  const [items, setItems] = useState<Item[]>([]);
  let connection: HubConnection | null = null;

  const fetchItems = () => {
    axios.get('https://localhost:44313/items')
      .then(response => {
        setItems(response.data.result);
      });
  };

  const createItem = (name: string) => {
    axios.post('https://localhost:44313/items', { name })
      .then(() => {
        fetchItems();
      });
  };

  const deleteItem = (id: number) => {
    axios.delete(`https://localhost:44313/items/${id}`)
      .then(() => {
        fetchItems();
      });
  };

  useEffect(() => {
    fetchItems();
  
    connection = new HubConnectionBuilder()
      .withUrl("https://localhost:44313/itemsHub")
      .build();
  
    connection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));
  
    connection.on("ReceiveItemsUpdate", () => {
      fetchItems();
    });
  
    return () => {
      if (connection) {
        connection.stop();
      }
    };
  }, []);

  return (
    <div>
      <input id="newItemName" />
      <button onClick={() => createItem((document.getElementById('newItemName') as HTMLInputElement).value)}>Create Item</button>
      <ul>
        {items.map(item => (
          <li key={item.id}>
            {item.name}
            <button onClick={() => deleteItem(item.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ItemList;