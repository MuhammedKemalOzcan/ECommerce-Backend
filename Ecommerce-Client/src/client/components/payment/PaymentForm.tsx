import React, { useEffect, useRef } from "react";

interface IyzicoPaymentFormProps {
  htmlContent: string;
}

const IyzicoPaymentForm: React.FC<IyzicoPaymentFormProps> = ({
  htmlContent,
}) => {
  const paymentContainerRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    if (!htmlContent || !paymentContainerRef.current) return;

    const scriptRegex = /<script type="text\/javascript">([\s\S]*?)<\/script>/;
    const match = htmlContent.match(scriptRegex);

    if (match && match[1]) {
      const scriptContent = match[1];

      const script = document.createElement("script");
      script.type = "text/javascript";
      script.innerHTML = scriptContent; 
      script.async = true;


      document.body.appendChild(script);

      return () => {
        document.body.removeChild(script);
      };
    }
  }, [htmlContent]);

  return (
    <div className="w-full flex justify-center items-center py-4">
      <div
        ref={paymentContainerRef}
        dangerouslySetInnerHTML={{ __html: htmlContent }}
      />
    </div>
  );
};

export default IyzicoPaymentForm;
