import React, { useEffect, useRef } from "react";

interface IyzicoPaymentFormProps {
  htmlContent: string;
}

const IyzicoPaymentForm: React.FC<IyzicoPaymentFormProps> = ({ htmlContent }) => {
  const isLoaded = useRef(false);

  useEffect(() => {
    if (!htmlContent || isLoaded.current) return;

    const scriptRegex = /<script type="text\/javascript">([\s\S]*?)<\/script>/;
    const match = htmlContent.match(scriptRegex);

    if (match && match[1]) {
      const scriptContent = match[1];

      const timeoutId = setTimeout(() => {
        const script = document.createElement("script");
        script.type = "text/javascript";
        script.innerHTML = `window.iyziInit = undefined; ${scriptContent}`;
        script.id = "iyzico-script-dynamic";
        
        document.body.appendChild(script);
        isLoaded.current = true;
      }, 100);

      return () => {
        clearTimeout(timeoutId);
        const oldScript = document.getElementById("iyzico-script-dynamic");
        if (oldScript) document.body.removeChild(oldScript);
        isLoaded.current = false;
      };
    }
  }, [htmlContent]);

  return (
    <div className="w-full flex justify-center items-center py-4 min-h-[400px]">
      <div
        id="iyzipay-checkout-form"
        className="responsive"
        dangerouslySetInnerHTML={{ __html: htmlContent.replace(/<script.*<\/script>/, "") }}
      />
    </div>
  );
};

export default IyzicoPaymentForm;